using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Excecao.ExcecaoBase;

namespace FidesOS.Aplicacao.CasoDeUsos.Senha.Recuperar;

public class RecuperarSenhaCasoDeUso : IRecuperarSenhaCasoDeUso
{
  private readonly IRepositorioLeituraUsuario _leituraUsuario;
  private readonly IRepositorioAlteracaoUsuario _alteracaoUsuario;
  private readonly IUnitOfWork _unitOfWork;

  public RecuperarSenhaCasoDeUso(
    IRepositorioLeituraUsuario leituraUsuario,
    IRepositorioAlteracaoUsuario alteracaoUsuario,
    IUnitOfWork unitOfWork)
  {
    _leituraUsuario = leituraUsuario;
    _alteracaoUsuario = alteracaoUsuario;
    _unitOfWork = unitOfWork;
  }

  public async Task<RespostaRecuperarSenhaJson> Execute(RequisicaoRecuperarSenhaJson request)
  {
    Validate(request);

    var usuario = await _leituraUsuario.BuscarPorEmail(request.Email);

    if (usuario is null)
      return new() { TokenDeRecuperacao = string.Empty };

    usuario.GerarTokenDeRecuperacao();

    await _unitOfWork.Commit();

    return new() { TokenDeRecuperacao = usuario.TokenRecuperacaoSenha! };
  }

  private void Validate(RequisicaoRecuperarSenhaJson request)
  {
    var validacao = new RecuperarSenhaValidacao().Validate(request);

    if (validacao.IsValid.IsFalse())
      throw new ErrorOnValidationException(validacao.Errors.Select(error => error.ErrorMessage).ToList());
  }
}
