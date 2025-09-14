using FluentMigrator;

namespace FidesOS.Infra.Migracao.Versoes;

[Migration(VersaoBancoDeDados.TRIGGER_ATUALIZADO_EM, "Cria trigger para atualizar automaticamente a coluna AtualizadoEm")]
public class Versao0000004 : VersaoBase
{
  private const string FUNCTION_NAME = "atualizar_coluna_atualizado_em";
  private const string TRIGGER_PREFIX = "trigger_atualizado_em_";

  public override void Up()
  {
    // 1. Cria a função que será reusada
    Execute.Sql($@"
            CREATE OR REPLACE FUNCTION {FUNCTION_NAME}()
            RETURNS TRIGGER AS $$
            BEGIN
                NEW.""AtualizadoEm"" = NOW();
                RETURN NEW;
            END;
            $$ LANGUAGE plpgsql;
        ");

    // 2. Aplica o trigger em cada tabela que precisa
    var tabelas = new List<string> { "Usuario", "OrdensDeServico", "AlocacoesTrabalhador", "DetalhesProducao", "RefreshTokens" };

    foreach (var tabela in tabelas)
    {
      Execute.Sql($@"
                CREATE TRIGGER {TRIGGER_PREFIX}{tabela}
                BEFORE UPDATE ON ""{tabela}""
                FOR EACH ROW
                EXECUTE FUNCTION {FUNCTION_NAME}();
            ");
    }
  }
}