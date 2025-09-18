using FidesOS.Comunicacao.Respostas;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Listar;

public interface IListarOrdensDeServicoCasoDeUso
{
  Task<RespostaPaginadaJson<RespostaOrdemDeServicoResumidaJson>> Execute(int paginaAtual, int itensPorPagina);
}
