using FidesOS.Dominio.Entidades;

namespace FidesOS.Dominio.Repositories.Usuarios;

/// <summary>
/// Contrato para operações de leitura de usuários.
/// </summary>
public interface IRepositorioLeituraUsuario
{
  Task<Usuario?> BuscarPorUserIdentificacao(Guid usuarioIdentificacao);

  Task<bool> ExisteEmpresaComUserIdentificacao(Guid id);

  Task<Usuario?> BuscarPorEmail(string email);

  Task<bool> ExisteUsuarioComEmail(string email);

  Task<Usuario?> BuscarPorTokenDeRecuperacao(string token);

  Task<IList<Usuario>> BuscarVariosPorIdentificacao(IList<Guid> ids);
}