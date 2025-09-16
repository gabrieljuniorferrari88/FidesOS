using FidesOS.Dominio.Entidades;
using FidesOS.Infra.AcessoDados.Configuracao;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebApi.Tests")]
namespace FidesOS.Infra.AcessoDados;

internal sealed class FidesOSDbContext : DbContext
{
  public FidesOSDbContext(DbContextOptions options) : base(options) { }

  public DbSet<Usuario> Usuarios { get; set; }
  public DbSet<RefreshToken> RefreshTokens { get; set; }
  public DbSet<OrdemDeServico> OrdensDeServico { get; set; }
  public DbSet<AlocacaoTrabalhador> AlocacoesTrabalhador { get; set; }
  public DbSet<DetalheProducao> DetalhesProducao { get; set; }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(FidesOSDbContext).Assembly);

    base.OnModelCreating(modelBuilder);
  }
}

