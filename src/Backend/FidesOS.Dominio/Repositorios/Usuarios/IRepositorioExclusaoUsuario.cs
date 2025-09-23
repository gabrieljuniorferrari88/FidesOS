namespace FidesOS.Dominio.Repositories.Usuarios;

public interface IRepositorioExclusaoUsuario
{
  void DeleteAccount(Guid userIdentifier);
}