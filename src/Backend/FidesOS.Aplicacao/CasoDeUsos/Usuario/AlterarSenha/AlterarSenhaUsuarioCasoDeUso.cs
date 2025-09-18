using FidesOS.Comunicacao.Requisicoes.Usuario;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Seguranca.Criptografia;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using FluentValidation.Results;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.AlterarSenha;

public class AlterarSenhaUsuarioCasoDeUso : IAlterarSenhaUsuarioCasoDeUso
{
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IRepositorioAlteracaoUsuario _repositorio;
  private readonly ISenhaCriptografada _senhaCriptografada;
  private readonly IUnitOfWork _unitOfWork;

  public AlterarSenhaUsuarioCasoDeUso(
    IUsuarioLogado usuarioLogado,
    IRepositorioAlteracaoUsuario repositorio,
    ISenhaCriptografada senhaCriptografada, 
    IUnitOfWork unitOfWork)
  {
    _usuarioLogado = usuarioLogado;
    _repositorio = repositorio;
    _senhaCriptografada = senhaCriptografada;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(RequisicaoAlterarSenhaUsuarioJson request)
  {
    var usuarioLogado = await _usuarioLogado.Get();

    Validate(request, usuarioLogado);

    var usuario = await _repositorio.BuscarPorUserIdentificacao(usuarioLogado.UserIdentificacao);

    usuario.AlterarSenha(_senhaCriptografada.Encrypt(request.NovaSenha));

    await _unitOfWork.Commit();
  }

  private void Validate(RequisicaoAlterarSenhaUsuarioJson request, Dominio.Entidades.Usuario usuarioLogado)
  {
    var validacao = new AlterarSenhaUsuarioValidacao();

    var resultado = validacao.Validate(request);

    var senhaCorrespondeComAtual = _senhaCriptografada.IsValid(request.Senha, usuarioLogado.Senha);

    if (senhaCorrespondeComAtual.IsFalse())
      resultado.Errors.Add(new ValidationFailure(string.Empty, ResourceMensagensExcecao.SENHA_ATUAL_DIFERENTE_DA_ATUAL));

    if(resultado.IsValid.IsFalse())
    {
      var errors = resultado.Errors.Select(e => e.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errors);
    }
  }
}
