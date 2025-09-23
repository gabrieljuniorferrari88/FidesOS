using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.AlocarTrabalhador;
using FidesOS.Comunicacao.Respostas.OrdemDeServico.AlocarTrabalhador;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.AlocarTrabalhador;

public interface IAlocarTrabalhadorCasoDeUso
{
  Task<RespostaAlocacaoJson> Execute(RequisicaoAlocarTrabalhadorJson request, Guid osId);
}