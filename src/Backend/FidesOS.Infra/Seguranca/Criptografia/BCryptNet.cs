using FidesOS.Dominio.Seguranca.Criptografia;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CommonTestUtilities")]
namespace FidesOS.Infra.Seguranca.Criptografia;

internal sealed class BCryptNet : ISenhaCriptografada
{
  public string Encrypt(string senha) => BCrypt.Net.BCrypt.HashPassword(senha);

  public bool IsValid(string senha, string senhaCriptografada) => BCrypt.Net.BCrypt.Verify(senha, senhaCriptografada);
}
