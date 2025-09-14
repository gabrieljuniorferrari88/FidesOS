using FidesOS.Dominio.Seguranca.Criptografia;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CommonTestUtilities")]
namespace FidesOS.Infra.Seguranca.Criptografia;

internal sealed class BCryptNet : ISenhaCriptografada
{
  public string Encrypt(string password) => BCrypt.Net.BCrypt.HashPassword(password);

  public bool IsValid(string password, string passwordHash) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}
