namespace FidesOS.Dominio.Entidades;
public class DetalheProducao : EntidadeBase
{
  public Guid DetalheIdentificacao { get; protected set; } = Guid.CreateVersion7();
  public Guid AlocacaoIdentificacao { get; protected set; } // FK para AlocacaoTrabalhador

  public string Descricao { get; protected set; } = string.Empty;

  // Valor em centavos
  public long Valor { get; protected set; }
}
