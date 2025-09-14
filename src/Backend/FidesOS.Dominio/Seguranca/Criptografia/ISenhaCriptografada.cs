namespace FidesOS.Dominio.Seguranca.Criptografia;

public interface ISenhaCriptografada
{
  public string Encrypt(string password);
  public bool IsValid(string password, string passwordHash);
}