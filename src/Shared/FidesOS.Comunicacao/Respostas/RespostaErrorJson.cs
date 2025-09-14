namespace FidesOS.Comunicacao.Respostas;

public class RespostaErrorJson
{
    public IList<string> Errors { get; set; }
    public bool TokenIsExpired { get; set; }

    public RespostaErrorJson(IList<string> errors) => Errors = errors;

    public RespostaErrorJson(string error)
    {
        Errors = new List<string>
        {
            error
        };
    }
}