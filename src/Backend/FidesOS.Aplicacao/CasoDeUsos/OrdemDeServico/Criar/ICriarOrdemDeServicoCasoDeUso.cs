using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Criar;

public interface ICriarOrdemDeServicoCasoDeUso
{
  Task<RespostaOrdemDeServicoJson> Execute(RequisicaoOrdemDeServicoJson request);
}
