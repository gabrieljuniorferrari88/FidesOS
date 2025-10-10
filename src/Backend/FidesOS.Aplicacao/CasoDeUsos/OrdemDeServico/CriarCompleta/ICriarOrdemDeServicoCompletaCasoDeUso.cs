using FidesOS.Comunicacao.Requisicoes.OrdemDeServico;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.CriarCompleta;

public interface ICriarOrdemDeServicoCompletaCasoDeUso
{
  Task<RespostaOrdemDeServicoDetalhadaJson> Execute(RequisicaoOrdemDeServicoCompletaJson request);
}