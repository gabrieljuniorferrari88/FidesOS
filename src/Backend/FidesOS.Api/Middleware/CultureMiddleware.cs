using FidesOS.Dominio.Extencoes;
using System.Globalization;

namespace FidesOS.Api.Middleware;

public class CultureMiddleware
{
  readonly RequestDelegate _next;

  private static readonly IList<string> SupportedCultures = new List<string>
    {
        "pt-BR", // nossa cultura principal
        "en-US"  // Ingles (Estados Unidos)
    };

  public CultureMiddleware(RequestDelegate next) => _next = next;

  public async Task Invoke(HttpContext context)
  {
    var cultureInfo = new CultureInfo("pt-BR"); // <<< Cultura padr�o

    var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

    if (requestedCulture.NotEmpty() && SupportedCultures.Contains(requestedCulture))
    {
      cultureInfo = new CultureInfo(requestedCulture);
    }

    CultureInfo.CurrentCulture = cultureInfo;
    CultureInfo.CurrentUICulture = cultureInfo;

    await _next(context);
  }
}