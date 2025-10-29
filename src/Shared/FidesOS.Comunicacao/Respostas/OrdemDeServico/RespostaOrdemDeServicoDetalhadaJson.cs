using FidesOS.Comunicacao.Respostas.OrdemDeServico.AlocarTrabalhador;
using FidesOS.Dominio.Enums;

namespace FidesOS.Comunicacao.Respostas.OrdemDeServico;

public class RespostaOrdemDeServicoDetalhadaJson
{
  public Guid Id { get; set; } // O Guid da OrdemDeServico
  public Guid EmpresaClienteId { get; set; } // O Guid da EmpresaCliente
  public string Descricao { get; set; } = string.Empty;
  public DateTime DataAgendamento { get; set; }
  public StatusOS Status { get; set; }

  // Lista aninhada com as alocações e seus detalhes
  public List<RespostaAlocacaoTrabalhadorJson> Alocacoes { get; set; } = [];
}