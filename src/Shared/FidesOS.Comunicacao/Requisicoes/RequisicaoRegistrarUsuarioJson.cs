using FidesOS.Dominio.Enums;

namespace FidesOS.Comunicacao.Requisicoes;
public class RequisicaoRegistrarUsuarioJson
{
  public string Nome { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Senha { get; set; } = string.Empty;
  public PerfilUsuario PerfilDesejado { get; set; }

}
