using FidesOS.Aplicacao.CasoDeUsos.FazerLogin;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Alterar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Buscar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Cancelar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Criar;
using FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Listar;
using FidesOS.Aplicacao.CasoDeUsos.Senha.Recuperar;
using FidesOS.Aplicacao.CasoDeUsos.Senha.Resetar;
using FidesOS.Aplicacao.CasoDeUsos.Token.RefreshToken;
using FidesOS.Aplicacao.CasoDeUsos.Usuario.Alterar;
using FidesOS.Aplicacao.CasoDeUsos.Usuario.AlterarSenha;
using FidesOS.Aplicacao.CasoDeUsos.Usuario.Perfil;
using FidesOS.Aplicacao.CasoDeUsos.Usuario.Registrar;
using FidesOS.Aplicacao.Servicos.Autenticacao;
using FidesOS.Aplicacao.Servicos.Mapeamentos;
using Microsoft.Extensions.DependencyInjection;

namespace FidesOS.Aplicacao;

public static class InjecaoDependenciaExtensao
{
  public static void AddApplication(this IServiceCollection services)
  {
    AddMapperConfigurations();
    AddUseCases(services);
    AddTokenService(services);
  }

  private static void AddMapperConfigurations()
  {
    ConfiguracaoDeMapeamentos.Configure();
  }

  private static void AddUseCases(IServiceCollection services)
  {
    services.AddScoped<IRegistrarUsuarioCasoDeUso, RegistrarUsuarioCasoDeUso>();
    services.AddScoped<IAlterarUsuarioCasoDeUso, AlterarUsuarioCasoDeUso>();
    services.AddScoped<IBuscarPerfilUsuarioCasoDeUso, BuscarPerfilUsuarioCasoDeUso>();
    services.AddScoped<IAlterarSenhaUsuarioCasoDeUso, AlterarSenhaUsuarioCasoDeUso>();

    services.AddScoped<IRecuperarSenhaCasoDeUso, RecuperarSenhaCasoDeUso>();
    services.AddScoped<IResetarSenhaCasoDeUso, ResetarSenhaCasoDeUso>();

    services.AddScoped<ICriarOrdemDeServicoCasoDeUso, CriarOrdemDeServicoCasoDeUso>();
    services.AddScoped<IListarOrdensDeServicoCasoDeUso, ListarOrdensDeServicoCasoDeUso>();
    services.AddScoped<IAlterarOrdemDeServicoCasoDeUso, AlterarOrdemDeServicoCasoDeUso>();
    services.AddScoped<ICancelarOrdemDeServicoCasoDeUso, CancelarOrdemDeServicoCasoDeUso>();
    services.AddScoped<IBuscarOrdemDeServicoCasoDeUso, BuscarOrdemDeServicoCasoDeUso>();

    services.AddScoped<IFazerLoginCasoDeUso, FazerLoginCasoDeUso> ();

    services.AddScoped<IUseRefreshTokenUseCase, UseRefreshTokenUseCase>();
  }

  private static void AddTokenService(IServiceCollection services) => services.AddScoped<ITokenServico, TokenServico>();

}
