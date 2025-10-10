using FidesOS.Comunicacao.Requisicoes.OrdemDeServico.AlocarTrabalhador;

namespace FidesOS.Comunicacao.Requisicoes.OrdemDeServico;

public class RequisicaoOrdemDeServicoCompletaJson
{
  public Guid EmpresaClienteId { get; set; }
  public string Descricao { get; set; } = string.Empty;
  public DateTime DataAgendamento { get; set; }
  public List<RequisicaoAlocarTrabalhadorJson> Alocacoes { get; set; } = [];
}