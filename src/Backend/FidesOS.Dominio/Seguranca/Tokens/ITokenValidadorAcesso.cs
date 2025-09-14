namespace FidesOS.Dominio.Seguranca.Tokens;
public interface ITokenValidadorAcesso
{
    void Validate(string token);
    Guid GetUserIdentifier(string token);
    Guid GetAccessTokenIdentifier(string token);
}
