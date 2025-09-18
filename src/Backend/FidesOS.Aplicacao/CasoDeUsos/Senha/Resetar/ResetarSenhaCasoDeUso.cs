using FidesOS.Comunicacao.Requisicoes.Senha;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Seguranca.Criptografia;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;

namespace FidesOS.Aplicacao.CasoDeUsos.Senha.Resetar;

public class ResetarSenhaCasoDeUso : IResetarSenhaCasoDeUso
{
  private readonly IRepositorioLeituraUsuario _leituraUsuario;
  private readonly IRepositorioAlteracaoUsuario _alteracaoUsuario;
  private readonly ISenhaCriptografada _senhaCriptografada;
  private readonly IUnitOfWork _unitOfWork;

  public ResetarSenhaCasoDeUso(
    IRepositorioLeituraUsuario leituraUsuario,
    IRepositorioAlteracaoUsuario alteracaoUsuario,
    ISenhaCriptografada senhaCriptografada,
    IUnitOfWork unitOfWork)
  {
    _leituraUsuario = leituraUsuario;
    _alteracaoUsuario = alteracaoUsuario;
    _senhaCriptografada = senhaCriptografada;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(RequisicaoResetarSenhaJson request)
  {
    var usuario = await Validate(request);

    usuario.AlterarSenha(_senhaCriptografada.Encrypt(request.NovaSenha));
    usuario.LimparTokenDeRecuperacao();

    await _unitOfWork.Commit();
  }

  private async Task<Dominio.Entidades.Usuario?> Validate(RequisicaoResetarSenhaJson request)
  {
    var validacao = new ResetarSenhaValidacao();
    var resultado = validacao.Validate(request);

    var usuario = await _leituraUsuario.BuscarPorTokenDeRecuperacao(request.TokenDeRecuperacao);

    var erros = new List<string>();

    if (usuario is null)
      erros.Add(ResourceMensagensExcecao.SEM_TOKEN);
    else if (usuario.DataExpiracaoToken <= DateTime.UtcNow) {
      erros.Add(ResourceMensagensExcecao.EXPIRADO_CODIGO);
      usuario.LimparTokenDeRecuperacao();
      await _unitOfWork.Commit();
    }

    erros.AddRange(resultado.Errors.Select(e => e.ErrorMessage));

    if (erros.Any())
      throw new ErrorOnValidationException(erros);

    return usuario;
  }
}
