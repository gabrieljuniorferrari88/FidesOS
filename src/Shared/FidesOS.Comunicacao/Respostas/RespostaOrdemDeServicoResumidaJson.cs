using FidesOS.Dominio.Enums;

namespace FidesOS.Comunicacao.Respostas;

public class RespostaOrdemDeServicoResumidaJson
{
  public Guid Id { get; set; }
  public string Descricao { get; set; } = string.Empty;
  public string DataAgendamento { get; set; } = string.Empty;
  public StatusOS Status { get; set; }

}
