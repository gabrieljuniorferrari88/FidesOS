using System.Net;

namespace FidesOS.Excecao.ExcecaoBase;

public class RefreshTokenExpiredException : FidesOSException
{
    public RefreshTokenExpiredException() : base(ResourceMensagensExcecao.INVALIDO_SESSAO)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
}