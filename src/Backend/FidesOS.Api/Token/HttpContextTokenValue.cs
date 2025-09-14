using FidesOS.Dominio.Seguranca.Tokens;

namespace FidesOS.Api.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContext;

    public HttpContextTokenValue(IHttpContextAccessor httpContext) => _httpContext = httpContext;

    public string Value()
    {
        var authentication = _httpContext.HttpContext!.Request.Headers.Authorization.ToString();

        return authentication["Bearer ".Length..].Trim();
    }
}