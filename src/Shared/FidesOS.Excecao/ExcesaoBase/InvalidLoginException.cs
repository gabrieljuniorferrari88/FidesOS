using System.Net;

namespace FidesOS.Excecao.ExcecaoBase;

public class InvalidLoginException : FidesOSException
{
    public InvalidLoginException() : base(ResourceMensagensExcecao.EMAIL_OU_SENHA_INVALIDO)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}