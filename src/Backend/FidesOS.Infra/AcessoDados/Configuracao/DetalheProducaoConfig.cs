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

    builder.HasOne<AlocacaoTrabalhador>()
               .WithMany(a => a.Detalhes) // Aponta para a lista 'Detalhes' na outra entidade
               .HasForeignKey(d => d.AlocacaoIdentificacao) // A chave estrangeira é esta
               .HasPrincipalKey(a => a.AlocacaoIdentificacao); // E ela se conecta a esta chave
  }
}