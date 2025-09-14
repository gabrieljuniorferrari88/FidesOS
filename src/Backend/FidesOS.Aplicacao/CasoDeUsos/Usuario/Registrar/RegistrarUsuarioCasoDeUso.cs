using FidesOS.Aplicacao.Servicos.Autenticacao;
using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Repositorios.RefreshToken;
using FidesOS.Dominio.Seguranca.Criptografia;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using FluentValidation.Results;
using Mapster;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Registrar;

public class RegistrarUsuarioCasoDeUso : IRegistrarUsuarioCasoDeUso
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IRepositorioLeituraUsuario _userReadOnlyRepository;
  private readonly IRepositorioEscritaUsuario _repository;
  private readonly ISenhaCriptografada _passwordEncripter;
  private readonly ITokenServico _tokenService;
  private readonly IRefreshTokenRepositorioEscrita _refreshTokenRepository;

  public RegistrarUsuarioCasoDeUso(
      IUnitOfWork unitOfWork,
      IRepositorioEscritaUsuario repository,
      IRepositorioLeituraUsuario userReadOnlyRepository,
      ISenhaCriptografada passwordEncripter,
      ITokenServico tokenService,
      IRefreshTokenRepositorioEscrita refreshTokenRepository)
  {
    _unitOfWork = unitOfWork;
    _userReadOnlyRepository = userReadOnlyRepository;
    _repository = repository;
    _passwordEncripter = passwordEncripter;
    _tokenService = tokenService;
    _refreshTokenRepository = refreshTokenRepository;
  }

  public async Task<RespostaUsuarioRegistradoJson> Execute(RequisicaoRegistrarUsuarioJson request)
  {
    await Validate(request);

    var user = request.Adapt<Dominio.Entidades.Usuario>();
    user.SetSenhaCriptogragada(_passwordEncripter.Encrypt(request.Senha));
    user.DefinirGestor();

    var tokens = _tokenService.GenerateTokens(user);

    await _repository.Add(user);

    await _refreshTokenRepository.Add(new Dominio.Entidades.RefreshToken
    {
      UserIdentificacao = user.UserIdentificacao,
      Token = tokens.Refresh,
      AccessTokenId = tokens.AccessTokenId
    });

    await _unitOfWork.Commit();

    return new()
    {
      Id = user.UserIdentificacao,
      Nome = user.Nome,
      Tokens = new RespostaTokensJson
      {
        AccessToken = tokens.Access,
        RefreshToken = tokens.Refresh
      }
    };
  }

  private async Task Validate(RequisicaoRegistrarUsuarioJson request)
  {
    var result = new RegistrarUsuarioValidator().Validate(request);

    var emailExist = await _userReadOnlyRepository.ExisteUsuarioComEmail(request.Email);
    if (emailExist)
      result.Errors.Add(new ValidationFailure(string.Empty, ResourceMensagensExcecao.EMAIL_EXISTENTE));

    if (result.IsValid.IsFalse())
      throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
  }
}
