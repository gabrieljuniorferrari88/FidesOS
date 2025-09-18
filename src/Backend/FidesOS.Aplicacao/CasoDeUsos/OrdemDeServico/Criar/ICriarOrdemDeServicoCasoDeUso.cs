using FidesOS.Comunicacao.Requisicoes.OrdemDeServico;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Criar;

public interface ICriarOrdemDeServicoCasoDeUso
{
  Task<RespostaOrdemDeServicoJson> Execute(RequisicaoOrdemDeServicoJson request);
}
