namespace FidesOS.Dominio.Entidades;
public abstract class EntidadeBase
{
  public long Id { get; protected set; }
  public DateTime CriadoEm { get; protected set; } = DateTime.UtcNow;
  public DateTime? AtualizadoEm { get; protected set; }
}
