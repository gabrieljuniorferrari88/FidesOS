using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.DetalhesProducao;

namespace FidesOS.Comunicacao.Requisicoes.OrdemDeServico.AlocarTrabalhador;

public class RequisicaoAlocarTrabalhadorJson
{
  public Guid TrabalhadorIdentificacao { get; set; }
  public long ValorCombinado { get; set; }
  public List<RequisicaoDetalheProducaoJson>? Detalhes { get; set; } = [];
}