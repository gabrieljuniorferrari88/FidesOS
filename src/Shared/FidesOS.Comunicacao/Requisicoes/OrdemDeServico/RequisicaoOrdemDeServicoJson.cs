namespace FidesOS.Comunicacao.Requisicoes.OrdemDeServico;

public class RequisicaoOrdemDeServicoJson
{
  public Guid EmpresaClienteId { get; set; }

  public string Descricao { get; set; } = string.Empty;
  public DateTime DataAgendamento { get; set; }
}
