using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Repositorios.RefreshToken;
using Microsoft.EntityFrameworkCore;

namespace FidesOS.Infra.AcessoDados.Repositorios;

internal class RepositorioRefreshToken : IRefreshTokenRepositorioLeitura, IRefreshTokenRepositorioEscrita
{
  private readonly FidesOSDbContext _context;
  public RepositorioRefreshToken(FidesOSDbContext context)
  {
    _context = context;
  }

  public async Task Add(RefreshToken refreshToken)
  {
    await _context.RefreshTokens
        .Where(token => token.UserIdentificacao == refreshToken.UserIdentificacao)
        .ExecuteDeleteAsync();

    await _context.RefreshTokens.AddAsync(refreshToken);
  }

  public async Task<RefreshToken?> Get(string token)
  {
    return await _context.RefreshTokens
        .AsNoTracking()
        .Include(refreshToken => refreshToken.User)
        .FirstOrDefaultAsync(refreshToken => refreshToken.Token.Equals(token));
  }
}
