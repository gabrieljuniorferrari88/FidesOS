using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Seguranca.Tokens;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Infra.AcessoDados;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace FidesOS.Infra.Servicos.UsuarioLogado;

internal sealed class UsuarioLogado : IUsuarioLogado
{
  private readonly FidesOSDbContext _dbContext;
  private readonly ITokenProvider _tokenValue;


  public UsuarioLogado(FidesOSDbContext dbContext, ITokenProvider tokenValue)
  {
    _dbContext = dbContext;
    _tokenValue = tokenValue;
  }



  public async Task<Usuario> Get()
  {
    var tokenHandler = new JwtSecurityTokenHandler();

    var jwtSecurityToken = tokenHandler.ReadJwtToken(_tokenValue.Value());

    var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value;

    return await _dbContext
        .Usuarios
        .AsNoTracking()
        .FirstAsync(user => user.UserIdentificacao == Guid.Parse(identifier));
  }
}
