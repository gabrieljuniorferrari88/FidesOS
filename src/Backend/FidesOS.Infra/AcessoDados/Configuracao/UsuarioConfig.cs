using FidesOS.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidesOS.Infra.AcessoDados.Configuracao;

public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
{
  public void Configure(EntityTypeBuilder<Usuario> builder)
  {
    builder.ToTable("Usuario");
  }
}
