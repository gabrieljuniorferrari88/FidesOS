using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Repositorios.OrdensDeServicos.AlocarTrabalhador;

namespace FidesOS.Infra.AcessoDados.Repositorios;

internal sealed class RepositorioAlocacaoTrabalhador : IRepositorioEscritaAlocacao
{
  private readonly FidesOSDbContext _dbContext;

  public RepositorioAlocacaoTrabalhador(FidesOSDbContext dbContext) => _dbContext = dbContext;

  public async Task AddAsync(AlocacaoTrabalhador alocacao) => await _dbContext.AddAsync(alocacao);
}