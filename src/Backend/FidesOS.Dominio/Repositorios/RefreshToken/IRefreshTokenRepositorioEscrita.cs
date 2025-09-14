namespace FidesOS.Dominio.Repositorios.RefreshToken;

public interface IRefreshTokenRepositorioEscrita
{
  Task Add(Entidades.RefreshToken refreshToken);
}
