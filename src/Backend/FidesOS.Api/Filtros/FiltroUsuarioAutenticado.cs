using FidesOS.Comunicacao.Respostas;
using FidesOS.Dominio.Repositories.Usuarios;
using FidesOS.Dominio.Seguranca.Tokens;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace FidesOS.Api.Filtros;

public class FiltroUsuarioAutenticado : IAsyncAuthorizationFilter
{
    private readonly ITokenValidadorAcesso _accessTokenValidator;
  private readonly IRepositorioLeituraUsuario _repository;

    public FiltroUsuarioAutenticado(ITokenValidadorAcesso accessTokenValidator, IRepositorioLeituraUsuario repository)
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            _accessTokenValidator.Validate(token);

            var userIdentifier = _accessTokenValidator.GetUserIdentifier(token);

            var user = await _repository.BuscarPorUserIdentificacao(userIdentifier);
            if (user is null)
            {
                throw new UnauthorizedException(ResourceMensagensExcecao.USUARIO_SEM_PERMISSAO_ACESSAR_RECURSO);
            }
        }
        catch (SecurityTokenExpiredException)
        {
            var response = new RespostaErrorJson("TokenExpired")
            {
                TokenIsExpired = true
            };

            context.Result = new UnauthorizedObjectResult(response);
        }
        catch (UnauthorizedException unathorizedException)
        {
            context.Result = new UnauthorizedObjectResult(new RespostaErrorJson(unathorizedException.GetErrorMessages()));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new RespostaErrorJson(ResourceMensagensExcecao.USUARIO_SEM_PERMISSAO_ACESSAR_RECURSO));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if(string.IsNullOrWhiteSpace(authentication))
        {
            throw new UnauthorizedException(ResourceMensagensExcecao.SEM_TOKEN);
        }

        return authentication["Bearer ".Length..].Trim();
    }
}
