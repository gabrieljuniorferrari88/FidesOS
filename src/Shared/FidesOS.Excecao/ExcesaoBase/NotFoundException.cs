using System.Net;

namespace FidesOS.Excecao.ExcecaoBase;

public class NotFoundException : FidesOSException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}