using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Repositorios.OrdensDeServicos.AlocarTrabalhador;
using Microsoft.EntityFrameworkCore;

namespace FidesOS.Infra.AcessoDados.Repositorios;

internal sealed class RepositorioAlocacaoTrabalhador : IRepositorioEscritaAlocacao, IRepositorioAlteracaoAlocacao
{
  private readonly FidesOSDbContext _dbContext;

  public RepositorioAlocacaoTrabalhador(FidesOSDbContext dbContext) => _dbContext = dbContext;

  public async Task AddAsync(AlocacaoTrabalhador alocacao) => await _dbContext.AddAsync(alocacao);

  public async Task<AlocacaoTrabalhador?> BuscarPorId(Guid alocacaoId)
  {
    return await _dbContext.AlocacoesTrabalhador
    .Include(os => os.OrdemDeServico) // Carrega os Dados Pais (cabeçalho)
    .SingleOrDefaultAsync(alocacao => alocacao.AlocacaoIdentificacao == alocacaoId);
  }
}