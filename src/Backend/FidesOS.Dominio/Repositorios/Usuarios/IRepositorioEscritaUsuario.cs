namespace FidesOS.Dominio.Repositories.Usuarios;
public interface IRepositorioEscritaUsuario
{
  Task Add(Entidades.Usuario usuario);
}