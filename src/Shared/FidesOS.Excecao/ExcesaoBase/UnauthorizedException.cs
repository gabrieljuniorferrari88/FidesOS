using System.Net;

namespace FidesOS.Excecao.ExcecaoBase;

public class UnauthorizedException : FidesOSException
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}