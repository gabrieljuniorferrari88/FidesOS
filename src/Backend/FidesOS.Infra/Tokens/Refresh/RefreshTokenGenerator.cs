using FidesOS.Dominio.Seguranca.Tokens;
using System.Security.Cryptography;

namespace FidesOS.Infra.Tokens.Refresh;

internal sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
  public string Generate()
  {
    var token = RandomNumberGenerator.GetBytes(32);

    return Convert.ToBase64String(token);
  }
}
