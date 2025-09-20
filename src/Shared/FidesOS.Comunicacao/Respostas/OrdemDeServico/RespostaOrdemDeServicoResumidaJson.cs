using FidesOS.Dominio.Enums;

namespace FidesOS.Comunicacao.Respostas.OrdemDeServico;

public class RespostaOrdemDeServicoResumidaJson
{
  public Guid Id { get; set; }
  public string Descricao { get; set; } = string.Empty;
  public DateTime DataAgendamento { get; set; }
  public StatusOS Status { get; set; }

}
