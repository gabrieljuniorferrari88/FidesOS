using FidesOS.Dominio.Entidades;
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
    base.OnModelCreating(modelBuilder);
   
    modelBuilder.Entity<Usuario>().ToTable("Usuario");

    modelBuilder.Entity<RefreshToken>(entity =>
    {
      entity.ToTable("RefreshTokens");
      entity.HasKey(e => e.Id);

      entity.HasOne(refreshToken => refreshToken.User)
            .WithMany()
            .HasForeignKey(refreshToken => refreshToken.UserIdentificacao)
            .HasPrincipalKey(usuario => usuario.UserIdentificacao);
    });
  }
}

