using FluentMigrator;

namespace FidesOS.Infra.Migracao.Versoes;

[Migration(VersaoBancoDeDados.ALTERACAO_STATUS_DEFAULT, "Adiciona valor padrão 1 para a coluna Status na tabela OrdensDeServico")]
public class Versao0000006 : VersaoBase
{
  public override void Up()
  {
    Alter.Column("Status")
           .OnTable("OrdensDeServico")
           .AsInt16()
           .NotNullable()
           .WithDefaultValue(0);
  }
}
