namespace FidesOS.Comunicacao.Respostas.OrdemDeServico.AlocarTrabalhador;

public class RespostaAlocacaoJson
{
  public Guid Id { get; set; }
  public Guid TrabalhadorIdentificacao { get; set; }
  public long ValorTotal { get; set; }
}
