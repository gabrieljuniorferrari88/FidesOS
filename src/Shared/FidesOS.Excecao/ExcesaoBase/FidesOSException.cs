using System.Net;

namespace FidesOS.Excecao.ExcecaoBase;

public abstract class FidesOSException : SystemException
{
    protected FidesOSException(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}