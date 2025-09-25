using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Repositorios.OrdensDeServicos.DetalhesProducao;

namespace FidesOS.Infra.AcessoDados.Repositorios;

internal sealed class RepositorioDetalhesProducao : IRepositorioEscritaDetalheProducao
{
  private readonly FidesOSDbContext _dbContext;

  public RepositorioDetalhesProducao(FidesOSDbContext dbContext) => _dbContext = dbContext;

  public async Task AddAsync(DetalheProducao detalhe) => await _dbContext.AddAsync(detalhe);
}