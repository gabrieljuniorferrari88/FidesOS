namespace FidesOS.Dominio.Repositories.Usuarios;

public interface IRepositorioAlteracaoUsuario
{
  Task<Entidades.Usuario> BuscarPorUserIdentificacao(Guid id);
  void Update(Entidades.Usuario user);
}