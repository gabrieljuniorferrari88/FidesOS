using FidesOS.Dominio.Entidades;

namespace FidesOS.Dominio.Servicos.UsuarioLogado;
public interface IUsuarioLogado
{
    Task<Usuario> Get();
}