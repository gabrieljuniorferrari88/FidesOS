namespace FidesOS.Domain.Repositories.Usuarios;

public interface IRepositorioExclusaoUsuario
{
  void DeleteAccount(Guid userIdentifier);
}
