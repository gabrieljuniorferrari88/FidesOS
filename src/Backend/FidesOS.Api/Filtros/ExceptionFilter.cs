using FidesOS.Comunicacao.Respostas;
using FidesOS.Excecao;
using FidesOS.Excecao.ExcecaoBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FidesOS.Api.Filtros;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is FidesOSException fidesOSException)
            HandleProjectException(fidesOSException, context);
        else
            ThrowUnknowError(context);
    }

    private static void HandleProjectException(FidesOSException fidesOSException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)fidesOSException.GetStatusCode();
        context.Result = new ObjectResult(new RespostaErrorJson(fidesOSException.GetErrorMessages()));
    }

    private static void ThrowUnknowError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new RespostaErrorJson(ResourceMensagensExcecao.ERRO_DESCONHECIDO));
    }
}