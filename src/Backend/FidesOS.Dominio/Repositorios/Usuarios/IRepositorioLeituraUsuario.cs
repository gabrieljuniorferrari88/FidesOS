using FidesOS.Dominio.Entidades;

namespace FidesOS.Dominio.Repositories.Usuarios;

/// <summary>
/// Contrato para opera��es de leitura de usu�rios.
/// </summary>
public interface IRepositorioLeituraUsuario
{
  Task<Usuario?> BuscarPorUserIdentificacao(Guid usuarioIdentificacao);
  Task<Usuario?> BuscarPorEmail(string email);
  Task<bool> ExisteUsuarioComEmail(string email);
  Task<Usuario?> BuscarPorTokenDeRecuperacao(string token);
}