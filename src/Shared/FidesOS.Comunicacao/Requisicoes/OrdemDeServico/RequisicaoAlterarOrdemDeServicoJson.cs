namespace FidesOS.Comunicacao.Respostas.OrdemDeServico;

public class RequisicaoAlterarOrdemDeServicoJson
{
  public string Descricao { get; set; } = string.Empty;
  public DateTime DataAgendamento { get; set; }
}