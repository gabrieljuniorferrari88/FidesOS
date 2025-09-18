using FidesOS.Dominio.Enums;

namespace FidesOS.Comunicacao.Respostas.OrdemDeServico;

public class RespostaOrdemDeServicoJson
{
  public Guid Id { get; set; } // O Guid público da OS (OsIdentificacao)
  public string Descricao { get; set; } = string.Empty;
  public DateTime DataAgendamento { get; set; }
  public StatusOS Status { get; set; }
}
