using FidesOS.Dominio.Enums;
using Microsoft.Extensions.Configuration;

namespace FidesOS.Infra.Extensoes;

public static class ConfiguracaoExtensoes
{
  public static string ConnectionString(this IConfiguration configuration)
  {
    var databaseType = configuration.GetDatabaseType();

    if (databaseType == TipoBancoDados.MySql)
    {
      return configuration.GetConnectionString("ConnectionMySQL")!;
    }

    if (databaseType == TipoBancoDados.SqlServer)
    {
      return configuration.GetConnectionString("ConnectionMySQL")!;
    }

    return configuration.GetConnectionString("ConnectionPostgres")!;
  }

  public static TipoBancoDados GetDatabaseType(this IConfiguration configuration)
  {
    var databaseType = configuration.GetConnectionString("DatabaseType")!;

    return Enum.Parse<TipoBancoDados>(databaseType);
  }

  public static bool IsUnitTestEnviroment(this IConfiguration configuration)
  {
    _ = bool.TryParse(configuration.GetSection("InMemoryTests").Value, out bool inMemoryTests);

    return inMemoryTests;
  }
}