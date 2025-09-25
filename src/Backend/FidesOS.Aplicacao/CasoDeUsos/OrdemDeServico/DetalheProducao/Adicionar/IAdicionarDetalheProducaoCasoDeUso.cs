using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.DetalhesProducao;
using FidesOS.Comunicacao.Respostas.OrdemDeServico;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.DetalheProducao.Adicionar;

public interface IAdicionarDetalheProducaoCasoDeUso
{
  Task<RespostaDetalheProducaoJson> Execute(RequisicaoDetalheProducaoJson request, Guid osId, Guid alocacaoId);
}