using FidesOS.Comunicacao.Respostas.Token;

namespace FidesOS.Comunicacao.Respostas.Usuario;
public class RespostaUsuarioRegistradoJson
{
  public Guid Id { get; set; }
  public string Nome { get; set; } = string.Empty;
  public RespostaTokensJson Tokens { get; set; } = default!;
}
