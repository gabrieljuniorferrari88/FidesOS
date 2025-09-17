using FidesOS.Comunicacao.Respostas;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Listar;

public interface IListarOrdensDeServicoCasoDeUso
{
  Task<RespostaPaginadaJson<RespostaOrdemDeServicoResumidaJson>> Execute(int paginaAtual, int itensPorPagina);
}
