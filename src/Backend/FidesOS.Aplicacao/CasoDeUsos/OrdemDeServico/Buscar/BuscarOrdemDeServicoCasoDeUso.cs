using FidesOS.Comunicacao.Respostas.OrdemDeServico;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using Mapster;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Buscar;

public class BuscarOrdemDeServicoCasoDeUso : IBuscarOrdemDeServicoCasoDeUso
{
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IRepositorioLeituraOrdemDeServico _ordemDeServico;

  public BuscarOrdemDeServicoCasoDeUso(
    IUsuarioLogado usuarioLogado, 
    IRepositorioLeituraOrdemDeServico ordemDeServico)
  {
    _usuarioLogado = usuarioLogado;
    _ordemDeServico = ordemDeServico;
  }

  public async Task<RespostaOrdemDeServicoJson> Execute(Guid osId)
  {
    var usuarioLogado = await _usuarioLogado.Get();

    var os = await _ordemDeServico.BuscarPorId(osId);

    Validate(os, usuarioLogado.UserIdentificacao);

    var resposta = os.Adapt<Dominio.Entidades.OrdemDeServico>();

    return new RespostaOrdemDeServicoJson
    {
      DataAgendamento = resposta.DataAgendamento,
      Descricao = resposta.Descricao,
      Id = resposta.OsIdentificacao,
      Status = resposta.Status
    };
  }

  private void Validate(Dominio.Entidades.OrdemDeServico? os, Guid userIdentificacao)
  {
    if (os is null)
      throw new ErrorOnValidationException(new List<string> { ResourceMensagensExcecao.NAO_ENCONTRADO });

    if (os.GestorIdentificacao != userIdentificacao)
      throw new UnauthorizedException(ResourceMensagensExcecao.USUARIO_SEM_PERMISSAO_ACESSAR_RECURSO);
  }
}
