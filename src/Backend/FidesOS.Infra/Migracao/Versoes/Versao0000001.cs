using FidesOS.Infra.Migracao;
using FidesOS.Infra.Migracao.Versoes;
using FluentMigrator;

namespace NextSind.Infrastructure.Migrations.Versions;

[Migration(VersaoBancoDeDados.TABELA_USUARIO, "Criando a tabela usuario")]
public class Versao0000001 : VersaoBase
{
  public override void Up()
  {
    CreateTable("Usuario")
            .WithColumn("Nome").AsString(255).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable().Unique() // <<< CORRIGIDO
            .WithColumn("Senha").AsString(2000).NotNullable()
            .WithColumn("UserIdentificacao").AsGuid().NotNullable().Unique() // <<< CORRIGIDO
            .WithColumn("GestorIdentificacao").AsGuid().NotNullable().Indexed()
            .WithColumn("Perfil").AsInt16().NotNullable()
            .WithColumn("Status").AsInt16().NotNullable()
            .WithColumn("AvatarUrl").AsString(2000).Nullable();

    CreateTable("RefreshTokens")
            .WithColumn("Token").AsString(1000).NotNullable()
            .WithColumn("AccessTokenId").AsGuid().NotNullable()
            .WithColumn("UserIdentificacao").AsGuid().NotNullable().ForeignKey("FK_RefreshTokens_User_Identificacao", "Usuario", "UserIdentificacao");
  }
}