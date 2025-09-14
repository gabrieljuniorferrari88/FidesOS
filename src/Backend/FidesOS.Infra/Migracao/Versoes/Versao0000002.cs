using FidesOS.Infra.Migracao;
using FidesOS.Infra.Migracao.Versoes;
using FluentMigrator;

namespace FidesOS.Infrastructure.Migrations.Versions;

[Migration(VersaoBancoDeDados.TABELAS_ORDEM_SERVICO, "Criando tabelas de Ordem de Serviço, Alocações e Detalhes")]
public class Versao0000002 : VersaoBase
{
  public override void Up()
  {
    CreateTable("OrdensDeServico")
            .WithColumn("OsIdentificacao").AsGuid().NotNullable().Unique()
            .WithColumn("GestorIdentificacao").AsGuid().NotNullable().Indexed()
            .WithColumn("EmpresaClienteId").AsGuid().NotNullable().Indexed()
            .WithColumn("DataAgendamento").AsDateTime().NotNullable()
            .WithColumn("Status").AsInt16().NotNullable().Indexed()
            .WithColumn("Descricao").AsString(1000).NotNullable();

    CreateTable("AlocacoesTrabalhador")
        .WithColumn("AlocacaoIdentificacao").AsGuid().NotNullable().Unique()
        .WithColumn("OsIdentificacao").AsGuid().NotNullable().Indexed()
        .WithColumn("TrabalhadorIdentificacao").AsGuid().NotNullable().Indexed()
        .WithColumn("InicioTrabalho").AsDateTime().Nullable()
        .WithColumn("FimTrabalho").AsDateTime().Nullable()
        .WithColumn("ValorCombinado").AsInt64().NotNullable()
        .WithColumn("ValorAcrescimo").AsInt64().NotNullable()
        .WithColumn("ValorDesconto").AsInt64().NotNullable()
        .WithColumn("ValorTotal").AsInt64().NotNullable()
        .WithColumn("StatusFaturamento").AsInt16().NotNullable()
        .WithColumn("StatusPagamento").AsInt16().NotNullable();

    CreateTable("DetalhesProducao")
        .WithColumn("DetalheIdentificacao").AsGuid().NotNullable().Unique()
        .WithColumn("AlocacaoIdentificacao").AsGuid().NotNullable().Indexed()
        .WithColumn("Descricao").AsString(500).NotNullable()
        .WithColumn("Valor").AsInt64().NotNullable();
  }
}