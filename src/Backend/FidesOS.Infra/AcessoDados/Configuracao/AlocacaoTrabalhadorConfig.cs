using FidesOS.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidesOS.Infra.AcessoDados.Configuracao;

public class AlocacaoTrabalhadorConfig : IEntityTypeConfiguration<AlocacaoTrabalhador>
{
  public void Configure(EntityTypeBuilder<AlocacaoTrabalhador> builder)
  {
    builder.ToTable("AlocacoesTrabalhador");
    builder.HasKey(a => a.Id);

    // Relacionamento com OrdemDeServico
    builder.HasOne<OrdemDeServico>()
          .WithMany(os => os.Alocacoes)
          .HasForeignKey(a => a.OsIdentificacao)
          .HasPrincipalKey(os => os.OsIdentificacao);

    // Relacionamento com Usuario (Trabalhador)
    builder.HasOne<Usuario>()
          .WithMany()
          .HasForeignKey(a => a.TrabalhadorIdentificacao)
          .HasPrincipalKey(usuario => usuario.UserIdentificacao);
  }
}