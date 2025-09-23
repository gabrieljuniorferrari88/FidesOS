using FidesOS.Dominio.Enums;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Repositories;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Repositorios.OrdensDeServicos;
using FidesOS.Dominio.Repositorios.OrdensDeServicos.AlocarTrabalhador;
using FidesOS.Dominio.Repositorios.RefreshToken;
using FidesOS.Dominio.Seguranca.Criptografia;
using FidesOS.Dominio.Seguranca.Tokens;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using FidesOS.Infra.AcessoDados;
using FidesOS.Infra.AcessoDados.Repositorios;
using FidesOS.Infra.Extensoes;
using FidesOS.Infra.Seguranca.Criptografia;
using FidesOS.Infra.Servicos.UsuarioLogado;
using FidesOS.Infra.Tokens.Acesso.Gerador;
using FidesOS.Infra.Tokens.Acesso.Validacao;
using FidesOS.Infra.Tokens.Refresh;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FidesOS.Infra;

public static class InjecaoDependenciaExtensao
{
  public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
  {
    AddRepositories(services, environment);
    AddLoggedUser(services);
    AddTokenHandlers(services, configuration);
    AddPasswordEncripter(services);

    if (environment.IsTests().IsFalse())
    {
      AddDbContext(services, configuration);
      AddFluentMigrator(services, configuration);
    }
  }

  private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.ConnectionString();

    services.AddDbContext<FidesOSDbContext>(dbContextOptions =>
    {
      var databaseType = configuration.GetDatabaseType();

      if (databaseType is TipoBancoDados.MySql)
        dbContextOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
      else if (databaseType is TipoBancoDados.SqlServer)
        dbContextOptions.UseSqlServer(connectionString);
      else
        dbContextOptions.UseNpgsql(connectionString);
    });
  }

  private static void AddRepositories(IServiceCollection services, IWebHostEnvironment environment)
  {
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // Registra o Unit of Work
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    // Registra o repositório único que implementa todas as interfaces de usuário

    services.AddScoped<IRepositorioLeituraUsuario, UsuarioRepositorio>();
    services.AddScoped<IRepositorioEscritaUsuario, UsuarioRepositorio>();
    services.AddScoped<IRepositorioAlteracaoUsuario, UsuarioRepositorio>();

    services.AddScoped<IRepositorioEscritaOrdemDeServico, RepositorioOrdemDeServico>();
    services.AddScoped<IRepositorioLeituraOrdemDeServico, RepositorioOrdemDeServico>();
    services.AddScoped<IRepositorioAlteracaoOrdemDeServico, RepositorioOrdemDeServico>();

    services.AddScoped<IRepositorioEscritaAlocacao, RepositorioAlocacaoTrabalhador>();

    services.AddScoped<IRefreshTokenRepositorioLeitura, RepositorioRefreshToken>();

    if (environment.IsTests().IsFalse())
      services.AddScoped<IRefreshTokenRepositorioEscrita, RepositorioRefreshToken>();
  }

  private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<IUsuarioLogado, UsuarioLogado>();

  private static void AddPasswordEncripter(IServiceCollection services)
  {
    services.AddScoped<ISenhaCriptografada, BCryptNet>();
  }

  private static void AddTokenHandlers(IServiceCollection services, IConfiguration configuration)
  {
    var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
    var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey")!;

    services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

    services.AddScoped<ITokenValidadorAcesso>(option => new JwtTokenValidator(signingKey));
    services.AddScoped<ITokenGeradorAcesso>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey));
  }

  private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.ConnectionString();
    var databaseType = configuration.GetDatabaseType();

    services.AddFluentMigratorCore().ConfigureRunner(config =>
    {
      var infrastructure = Assembly.Load("FidesOS.Infra");

      var migrationRunnerBuilder = databaseType switch
      {
        TipoBancoDados.MySql => config.AddMySql5(),
        TipoBancoDados.SqlServer => config.AddSqlServer(),
        _ => config.AddPostgres()
      };

      migrationRunnerBuilder
      .WithGlobalConnectionString(connectionString)
      .ScanIn(infrastructure)
      .For.All();
    });
  }
}