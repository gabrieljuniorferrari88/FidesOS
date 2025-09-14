using FidesOS.Dominio.Repositories;

namespace FidesOS.Infra.AcessoDados;

internal sealed class UnitOfWork : IUnitOfWork
{
  private readonly FidesOSDbContext _dbContext;

  public UnitOfWork(FidesOSDbContext dbContext) => _dbContext = dbContext;

  public async Task Commit() => await _dbContext.SaveChangesAsync();
}
