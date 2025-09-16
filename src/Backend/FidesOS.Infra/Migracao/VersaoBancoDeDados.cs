namespace FidesOS.Infra.Migracao;

public abstract class VersaoBancoDeDados
{
  public const int TABELA_USUARIO = 1;
  public const int TABELAS_ORDEM_SERVICO = 2; 
  public const int RELACIONAMENTOS_TABELAS_ORDEM_SERVICO = 3;
  public const int TRIGGER_ATUALIZADO_EM = 4;
  public const int TOKEN_RECUPERACAO_SENHA_USUARIO = 5;
  public const long ALTERACAO_STATUS_DEFAULT = 6;
}
