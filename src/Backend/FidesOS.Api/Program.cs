using FidesOS.Api.Converters;
using FidesOS.Api.Filtros;
using FidesOS.Api.Middleware;
using FidesOS.Api.Token;
using FidesOS.Aplicacao;
using FidesOS.Dominio.Extencoes;
using FidesOS.Dominio.Seguranca.Tokens;
using FidesOS.Infra;
using FidesOS.Infra.Extensoes;
using FidesOS.Infra.Migracao;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var webAppUrl = "http://localhost:3000";

builder.Services.AddCors(options =>
{
    options.AddPolicy("WebAppPolicy", policy =>
    {
        policy.WithOrigins(webAppUrl) // Permite que a URL do seu frontend acesse
              .AllowAnyHeader()        // Permite qualquer cabeçalho (como Authorization)
              .AllowAnyMethod();       // Permite qualquer método (GET, POST, PUT, etc.)
    });
});

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
  config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
    In = ParameterLocation.Header,
    Scheme = "Bearer",
    Type = SecuritySchemeType.ApiKey
  });

  config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddMvc(options => options.Filters.Add<ExceptionFilter>());

builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();

  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseCors("WebAppPolicy");

app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsTests().IsFalse())
{
  await MigrateDatabase();
}

app.Run();

async Task MigrateDatabase()
{
  await using var scope = app.Services.CreateAsyncScope();

  var databaseType = builder.Configuration.GetDatabaseType();
  var stringConnection = builder.Configuration.ConnectionString();

  MigracaoBancoDeDados.Migrate(databaseType, stringConnection, scope.ServiceProvider);
}

public partial class Program
{

}