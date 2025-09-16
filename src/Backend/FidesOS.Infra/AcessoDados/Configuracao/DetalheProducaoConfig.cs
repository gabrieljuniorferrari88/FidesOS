using FidesOS.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidesOS.Infra.AcessoDados.Configuracao;

public class DetalheProducaoConfig : IEntityTypeConfiguration<DetalheProducao>
{
  public void Configure(EntityTypeBuilder<DetalheProducao> builder)
  {
    builder.ToTable("DetalhesProducao");
    builder.HasKey(d => d.Id);

    // Relacionamento com AlocacaoTrabalhador
    builder.HasOne<AlocacaoTrabalhador>()
          .WithMany() // TODO: Adicionar a lista de Detalhes na entidade AlocacaoTrabalhador
          .HasForeignKey(d => d.AlocacaoIdentificacao)
          .HasPrincipalKey(a => a.AlocacaoIdentificacao);
  }
}
