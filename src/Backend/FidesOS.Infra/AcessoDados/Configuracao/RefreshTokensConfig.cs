using FidesOS.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidesOS.Infra.AcessoDados.Configuracao;

public class RefreshTokensConfig : IEntityTypeConfiguration<RefreshToken>
{
  public void Configure(EntityTypeBuilder<RefreshToken> builder)
  {
    builder.ToTable("RefreshTokens");

    builder.HasKey(e => e.Id);

    builder.HasOne(refreshToken => refreshToken.User)
          .WithMany()
          .HasForeignKey(refreshToken => refreshToken.UserIdentificacao)
          .HasPrincipalKey(usuario => usuario.UserIdentificacao);
  }
}
