using Dapper;
using FidesOS.Dominio.Enums;
using FidesOS.Dominio.Extencoes;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using Npgsql;

namespace FidesOS.Infra.Migracao;

public static class MigracaoBancoDeDados
{
  public static void Migrate(TipoBancoDados databaseType, string connectionString, IServiceProvider serviceProvider)
  {

    if (databaseType is TipoBancoDados.MySql)
      EnsureDatabaseCreatedForMySql(connectionString);
    else if (databaseType is TipoBancoDados.SqlServer)
      EnsureDatabaseCreatedForSQLServer(connectionString);
    else
      EnsureDatabaseCreatedForPostgres(connectionString);

    MigrateDatabase(serviceProvider);
  }

  private static void EnsureDatabaseCreatedForPostgres(string connectionString)
  {
    var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);

    var databaseName = connectionStringBuilder.Database;

    connectionStringBuilder.Remove("Database");
    connectionStringBuilder.Database = "postgres";

    using var dbConnection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);

    var parameters = new DynamicParameters();
    parameters.Add("name", databaseName);

    var records = dbConnection.Query(
        "SELECT 1 FROM pg_database WHERE datname = @name", parameters);

    // <<< A CORREÇÃO ESTÁ AQUI
    // Verifica se a lista de registros está vazia (ou seja, se o banco NÃO existe)
    if (!records.Any())
    {
      dbConnection.Execute($"CREATE DATABASE \"{databaseName}\"");
    }
  }

  private static void EnsureDatabaseCreatedForSQLServer(string connectionString)
  {
    var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

    var databaseName = connectionStringBuilder.InitialCatalog;

    connectionStringBuilder.Remove("Initial Catalog");

    using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

    var parameters = new DynamicParameters();
    parameters.Add("name", databaseName);

    var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

    if (records.Any() == false)
      dbConnection.Execute($"CREATE DATABASE {databaseName}");
  }

  private static void EnsureDatabaseCreatedForMySql(string connectionString)
  {
    var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);

    var databaseName = connectionStringBuilder.Database;

    connectionStringBuilder.Remove("Database");

    using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

    dbConnection.Execute($"CREATE DATABASE IF NOT EXISTS {databaseName}");
  }

  private static void MigrateDatabase(IServiceProvider serviceProvider)
  {
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

    runner.ListMigrations();

    runner.MigrateUp();
  }
}
