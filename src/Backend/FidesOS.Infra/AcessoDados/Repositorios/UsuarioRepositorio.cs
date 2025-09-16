using FidesOS.Dominio.Entidades;
using FidesOS.Dominio.Enums;
using FidesOS.Dominio.Repositories.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace FidesOS.Infra.AcessoDados.Repositorios;

internal sealed class UsuarioRepositorio : IRepositorioEscritaUsuario, IRepositorioLeituraUsuario, IRepositorioAlteracaoUsuario
{
  private readonly FidesOSDbContext _dbContext;

  public UsuarioRepositorio(FidesOSDbContext dbContext) => _dbContext = dbContext;

  public async Task Add(Usuario usuario) => await _dbContext.Usuarios.AddAsync(usuario);

  public async Task<bool> ExisteUsuarioComEmail(string email) => await _dbContext.Usuarios.AnyAsync(usuario => usuario.Email.Equals(email));

  async Task<Usuario> IRepositorioAlteracaoUsuario.BuscarPorUserIdentificacao(Guid id)
  {
    return await _dbContext
        .Usuarios
        .SingleAsync(usuario => usuario.UserIdentificacao == id);
  }

  async Task<Usuario?> IRepositorioLeituraUsuario.BuscarPorUserIdentificacao(Guid id)
  {
    return await _dbContext
        .Usuarios
        .AsNoTracking()
        .SingleOrDefaultAsync(usuario => usuario.UserIdentificacao == id);
  }

  public async Task<Usuario?> BuscarPorEmail(string email)
  {
    return await _dbContext
        .Usuarios
        .FirstOrDefaultAsync(usuario => usuario.Email.Equals(email));
  }

  public void Update(Usuario usuario) => _dbContext.Usuarios.Update(usuario);

  public async Task<Usuario?> BuscarPorTokenDeRecuperacao(string token)
  {
    return await _dbContext
        .Usuarios
        .SingleOrDefaultAsync(usuario => usuario.TokenRecuperacaoSenha!.Equals(token));
  }

  public async Task<bool> ExisteEmpresaComUserIdentificacao(Guid id)
  {
    return await _dbContext
        .Usuarios
        .Where(e => e.Perfil.Equals(PerfilUsuario.Empresa))
        .AnyAsync(e => e.UserIdentificacao.Equals(id));
  }
}
