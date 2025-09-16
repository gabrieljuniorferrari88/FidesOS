using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;

namespace FidesOS.Infra.AcessoDados.Repositorios;

internal sealed class RepositorioOrdemDeServico : IRepositorioEscritaOrdemDeServico
{
  private readonly FidesOSDbContext _dbContext;

  public RepositorioOrdemDeServico(FidesOSDbContext dbContext) =>  _dbContext = dbContext;

  public async Task AddAsync(OrdemDeServico os) => await _dbContext.AddAsync(os);
}

