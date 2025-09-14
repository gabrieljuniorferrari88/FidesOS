namespace FidesOS.Dominio.Entidades;

public class RefreshToken : EntidadeBase
{
  public string Token { get; set; } = string.Empty;
  public Guid AccessTokenId { get; set; }
  public Guid UserIdentificacao { get; set; }
  public Usuario User { get; set; } = default!;
}
