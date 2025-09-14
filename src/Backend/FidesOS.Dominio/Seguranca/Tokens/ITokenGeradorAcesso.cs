using FidesOS.Dominio.Entidades;

namespace FidesOS.Dominio.Seguranca.Tokens;
public interface ITokenGeradorAcesso
{
    (string token, Guid accessTokenIdentifier) Generate(Usuario usuario);
}
