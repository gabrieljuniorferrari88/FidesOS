namespace FidesOS.Dominio.Repositorios.RefreshToken;

public interface IRefreshTokenRepositorioLeitura
{
  Task<Entidades.RefreshToken?> Get(string token);
}
