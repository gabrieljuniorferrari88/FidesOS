using FidesOS.Aplicacao.CasoDeUsos.FazerLogin;
using FidesOS.Aplicacao.CasoDeUsos.Token.RefreshToken;
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
    //services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
    //services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
    //services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
    //services.AddScoped<IChangeUserPhotoUseCase, ChangeUserPhotoUseCase>();

    services.AddScoped<IFazerLoginCasoDeUso, FazerLoginCasoDeUso> ();

    //services.AddScoped<IRegisterWorkItemUseCase, RegisterWorkItemUseCase>();
    //services.AddScoped<IDeleteWorkItemUseCase, DeleteWorkItemUseCase>();
    //services.AddScoped<IUpdateWorkItemUseCase, UpdateWorkItemUseCase>();
    //services.AddScoped<IGetByIdWorkItemUseCase, GetByIdWorkItemUseCase>();
    //services.AddScoped<IGetAllWorkItemUseCase, GetAllWorkItemUseCase>();

    //services.AddScoped<IGetDashboardUseCase, GetDashboardUseCase>();

    services.AddScoped<IUseRefreshTokenUseCase, UseRefreshTokenUseCase>();
  }

  private static void AddTokenService(IServiceCollection services) => services.AddScoped<ITokenServico, TokenServico>();

}
