namespace FidesOS.Comunicacao.Respostas.OrdemDeServico;

public class RespostaDetalheProducaoJson
{
  public Guid Id { get; set; }
  public string Descricao { get; set; } = string.Empty;
  public long Valor { get; set; }
}