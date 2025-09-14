using FidesOS.Infra.Migracao;
using FidesOS.Infra.Migracao.Versoes;
using FluentMigrator;

namespace FidesOS.Infrastructure.Migrations.Versions;

[Migration(VersaoBancoDeDados.RELACIONAMENTOS_TABELAS_ORDEM_SERVICO, "Criando os relacionamentos das tabelas de Ordem de Serviço, Alocações e Detalhes")]
public class Versao0000003 : VersaoBase
{
  public override void Up()
  {
    // --- Criando as Chaves Estrangeiras (Foreign Keys) ---

    // Na tabela OrdensDeServico
    Create.ForeignKey("FK_OrdemServico_Usuario_Gestor")
        .FromTable("OrdensDeServico").ForeignColumn("GestorIdentificacao")
        .ToTable("Usuario").PrimaryColumn("UserIdentificacao");

    Create.ForeignKey("FK_OrdemServico_Usuario_Cliente")
        .FromTable("OrdensDeServico").ForeignColumn("EmpresaClienteId")
        .ToTable("Usuario").PrimaryColumn("UserIdentificacao");

    // Na tabela AlocacoesTrabalhador
    Create.ForeignKey("FK_Alocacao_OrdemServico")
        .FromTable("AlocacoesTrabalhador").ForeignColumn("OsIdentificacao")
        .ToTable("OrdensDeServico").PrimaryColumn("OsIdentificacao");

    Create.ForeignKey("FK_Alocacao_Usuario_Trabalhador")
        .FromTable("AlocacoesTrabalhador").ForeignColumn("TrabalhadorIdentificacao")
        .ToTable("Usuario").PrimaryColumn("UserIdentificacao"); 

    // Na tabela DetalhesProducao
    Create.ForeignKey("FK_Detalhe_Alocacao")
        .FromTable("DetalhesProducao").ForeignColumn("AlocacaoIdentificacao")
        .ToTable("AlocacoesTrabalhador").PrimaryColumn("AlocacaoIdentificacao");
  }
}