namespace FidesOS.Comunicacao.Respostas.OrdemDeServico.AlocarTrabalhador;

public class RespostaAlocacaoTrabalhadorJson
{
  public Guid Id { get; set; } // O Guid da AlocacaoTrabalhador
  public Guid TrabalhadorIdentificacao { get; set; }
  public long ValorTotal { get; set; }

  // Lista aninhada com os detalhes
  public List<RespostaDetalheProducaoJson> Detalhes { get; set; } = [];
}