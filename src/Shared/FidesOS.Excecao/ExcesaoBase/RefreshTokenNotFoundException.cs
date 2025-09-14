using System.Net;

namespace FidesOS.Excecao.ExcecaoBase;

public class RefreshTokenNotFoundException : FidesOSException
{
    public RefreshTokenNotFoundException() : base(ResourceMensagensExcecao.EXPIRADO_SESSAO)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}