using FidesOS.Comunicacao.Respostas;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using Mapster;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Listar;

public class ListarOrdensDeServicoCasoDeUso : IListarOrdensDeServicoCasoDeUso
{
  private readonly IUsuarioLogado _usuarioLogado;
  private readonly IRepositorioLeituraOrdemDeServico _repositorioLeitura;

  public ListarOrdensDeServicoCasoDeUso(
    IUsuarioLogado usuarioLogado, 
    IRepositorioLeituraOrdemDeServico repositorioLeitura)
  {
    _usuarioLogado = usuarioLogado;
    _repositorioLeitura = repositorioLeitura;
  }

  public async Task<RespostaPaginadaJson<RespostaOrdemDeServicoResumidaJson>> Execute(int paginaAtual, int itensPorPagina)
  {
    var usuarioLogado = await _usuarioLogado.Get();

    var listaDeEntidades = await _repositorioLeitura.ListarOSPorGestor(usuarioLogado.UserIdentificacao, paginaAtual, itensPorPagina);

    var listaDeRespostas = listaDeEntidades.Adapt<List<RespostaOrdemDeServicoResumidaJson>>();

    var totalItens = 0;
    if (listaDeEntidades.Any())
    {
      totalItens = await _repositorioLeitura.ContarOSPorGestor(usuarioLogado.UserIdentificacao);
    }

    var totalPaginas = (double)totalItens / itensPorPagina;

    return new RespostaPaginadaJson<RespostaOrdemDeServicoResumidaJson>
    {
      Itens = listaDeRespostas,
      PaginaAtual = paginaAtual,
      TotalDeItens = totalItens,
      TotalDePaginas = (int)Math.Ceiling(totalPaginas)
    };
  }
}
