using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Seguranca.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FidesOS.Infra.Tokens.Acesso.Gerador;

internal sealed class JwtTokenGenerator : JwtTokenHandler, ITokenGeradorAcesso
{
  private readonly uint _expirationTimeMinutes;
  private readonly string _signingKey;

  public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
  {
    _expirationTimeMinutes = expirationTimeMinutes;
    _signingKey = signingKey;
  }

  public (string token, Guid accessTokenIdentifier) Generate(Usuario user)
  {
    var accessTokenIdentifier = Guid.CreateVersion7();

    var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti, accessTokenIdentifier.ToString()),
            new (JwtRegisteredClaimNames.NameId, user.Id.ToString())
        };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
      Subject = new ClaimsIdentity(claims),
      SigningCredentials = new SigningCredentials(SecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
    };

    var tokenHandler = new JwtSecurityTokenHandler();

    var securityToken = tokenHandler.CreateToken(tokenDescriptor);

    return (tokenHandler.WriteToken(securityToken), accessTokenIdentifier);
  }
}