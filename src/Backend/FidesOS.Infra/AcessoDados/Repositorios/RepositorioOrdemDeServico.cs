using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;
using Microsoft.EntityFrameworkCore;

namespace FidesOS.Infra.AcessoDados.Repositorios;

internal sealed class RepositorioOrdemDeServico : IRepositorioEscritaOrdemDeServico, IRepositorioLeituraOrdemDeServico
{
  private readonly FidesOSDbContext _dbContext;

  public RepositorioOrdemDeServico(FidesOSDbContext dbContext) =>  _dbContext = dbContext;

  public async Task AddAsync(OrdemDeServico os) => await _dbContext.AddAsync(os);

  public async Task<int> ContarOSPorGestor(Guid gestorIdentificacao) => 
    await _dbContext.OrdensDeServico.CountAsync(os => os.GestorIdentificacao.Equals(gestorIdentificacao));

  public async Task<List<OrdemDeServico>> ListarOSPorGestor(Guid gestorIdentificacao, int pagina = 1, int itensPorPagina = 10)
  {
    var skip = (pagina - 1) * itensPorPagina;

    var lista = await _dbContext.OrdensDeServico
      .Where(os => os.GestorIdentificacao.Equals(gestorIdentificacao))
      .OrderByDescending(os => os.CriadoEm)
      .Skip(skip)
      .Take(itensPorPagina)
      .ToListAsync();

    return lista;
  }
}

