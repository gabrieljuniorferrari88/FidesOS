using FidesOS.Aplicacao.Servicos.Autenticacao;
using FidesOS.Comunicacao.Requisicoes.FazerLogin;
using FidesOS.Comunicacao.Respostas.Token;
using FidesOS.Comunicacao.Respostas.Usuario;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Repositorios.RefreshToken;
using FidesOS.Dominio.Seguranca.Criptografia;
using FidesOS.Excecao.ExcecaoBase;

namespace FidesOS.Aplicacao.CasoDeUsos.FazerLogin;

public class FazerLoginCasoDeUso : IFazerLoginCasoDeUso
{
  private readonly IRepositorioLeituraUsuario _repository;
  private readonly ISenhaCriptografada _passwordEncripter;
  private readonly ITokenServico _tokenService;
  private readonly IRefreshTokenRepositorioEscrita _refreshTokenRepository;
  private readonly IUnitOfWork _unitOfWork;

  public FazerLoginCasoDeUso(
      IRepositorioLeituraUsuario repository,
      ISenhaCriptografada passwordEncripter,
      ITokenServico tokenService,
      IRefreshTokenRepositorioEscrita refreshTokenRepository,
      IUnitOfWork unitOfWork)
  {
    _passwordEncripter = passwordEncripter;
    _repository = repository;
    _tokenService = tokenService;
    _refreshTokenRepository = refreshTokenRepository;
    _unitOfWork = unitOfWork;
  }
  public async Task<RespostaUsuarioRegistradoJson> Execute(RequisicaoLoginJson request)
  {
    var user = await _repository.BuscarPorEmail(request.Email);

    if (user is null)
      throw new InvalidLoginException();

    var passwordMatch = _passwordEncripter.IsValid(request.Senha, user.Senha);
    if (passwordMatch.IsFalse())
      throw new InvalidLoginException();

    var tokens = _tokenService.GenerateTokens(user);

    await _refreshTokenRepository.Add(new Dominio.Entidades.RefreshToken
    {
      UserIdentificacao = user.UserIdentificacao,
      Token = tokens.Refresh,
      AccessTokenId = tokens.AccessTokenId
    });

    await _unitOfWork.Commit();

    return new RespostaUsuarioRegistradoJson
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
}
