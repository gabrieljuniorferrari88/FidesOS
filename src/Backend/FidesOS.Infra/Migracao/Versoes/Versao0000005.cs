using FluentMigrator;

namespace FidesOS.Infra.Migracao.Versoes;

[Migration(VersaoBancoDeDados.TOKEN_RECUPERACAO_SENHA_USUARIO, "Alteracao da tabela usuario para receber tokens e data de expiracao do token.")]
public class Versao0000005 : VersaoBase
{
  public override void Up()
  {
    Alter.Table("Usuario")
        .AddColumn("TokenRecuperacaoSenha").AsString(255).Nullable()
        .AddColumn("DataExpiracaoToken").AsDateTime().Nullable();
  }
}