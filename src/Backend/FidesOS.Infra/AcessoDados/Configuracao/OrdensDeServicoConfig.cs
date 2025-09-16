using FidesOS.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidesOS.Infra.AcessoDados.Configuracao;

public class OrdensDeServicoConfig : IEntityTypeConfiguration<OrdemDeServico>
{
  public void Configure(EntityTypeBuilder<OrdemDeServico> builder)
  {
    builder.ToTable("OrdensDeServico");
    builder.HasKey(os => os.Id);

    // Relacionamento com Usuario (Gestor)
    // Uma OS tem um Gestor, mas um Gestor pode ter muitas OS.
    builder.HasOne<Usuario>() // Não especificamos a propriedade de navegação pois ela não existe
          .WithMany()
          .HasForeignKey(os => os.GestorIdentificacao)
          .HasPrincipalKey(usuario => usuario.UserIdentificacao);

    // Relacionamento com Usuario (EmpresaCliente)
    // Uma OS tem uma EmpresaCliente, mas uma EmpresaCliente pode ter muitas OS.
    builder.HasOne<Usuario>()
          .WithMany()
          .HasForeignKey(os => os.EmpresaClienteId)
          .HasPrincipalKey(usuario => usuario.UserIdentificacao);
  }
}
