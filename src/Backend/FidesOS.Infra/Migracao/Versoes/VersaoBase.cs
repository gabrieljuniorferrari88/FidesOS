using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace FidesOS.Infra.Migracao.Versoes;

public abstract class VersaoBase : ForwardOnlyMigration
{
  protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
  {
    return Create.Table(table)
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CriadoEm").AsDateTime().NotNullable()
            .WithColumn("AtualizadoEm").AsDateTime().Nullable();
  }
}
