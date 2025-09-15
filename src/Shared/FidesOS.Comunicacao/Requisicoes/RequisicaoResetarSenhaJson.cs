namespace FidesOS.Comunicacao.Requisicoes;

public class RequisicaoResetarSenhaJson
{
  public string TokenDeRecuperacao { get; set; } = string.Empty;
  public string NovaSenha { get; set; } = string.Empty;
}
