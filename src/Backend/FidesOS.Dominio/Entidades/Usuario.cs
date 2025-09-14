using FidesOS.Dominio.Enums;

namespace FidesOS.Dominio.Entidades;

public class Usuario : EntidadeBase
{
  public Guid UserIdentificacao { get; protected set; } = Guid.CreateVersion7();
  public Guid GestorIdentificacao { get; protected set; }

  public string Nome { get; protected set; } = string.Empty;
  public string Email { get; protected set; } = string.Empty;
  public string Senha { get; protected set; } = string.Empty;

  public PerfilUsuario Perfil { get; protected set; }
  public StatusUsuario Status { get; protected set; }

  public string? AvatarUrl { get; protected set; }

  // Adicione este método
  public void SetSenhaCriptogragada(string senha)
  {
    Senha = senha;
  }

  /// <summary>
  /// Método Para atualizar o Perfil do usuario logado
  /// </summary>
  /// <param name="novoNome"></param>
  /// <param name="novoEmail"></param>
  //// <returns>Sem retorno.</returns>
  //// <remarks>Deve ser implementado em cada classe derivada.</remarks>
  public void AtualizarPerfil(string novoNome, string novoEmail)
  {
    Nome = novoNome;
    Email = novoEmail;
    AtualizadoEm = DateTime.UtcNow;
  }

  public void AlterarAvatar(string? novaUrl)
  {
    AvatarUrl = novaUrl;
  }

  /// <summary>
  /// Define o GestorIdentificacao do usuário.
  /// Se o perfil for Gestor, ele se torna o próprio gestor.
  /// </summary>
  public void DefinirGestor()
  {
    if (Perfil == PerfilUsuario.Gestor)
    {
      GestorIdentificacao = UserIdentificacao;
    }
  }

  /// <summary>
  /// Define a qual gestor este usuário (Trabalhador/Empresa) pertence.
  /// </summary>
  public void AtribuirAGestor(Guid gestorIdentificacao)
  {
    if (Perfil != PerfilUsuario.Gestor)
    {
      GestorIdentificacao = gestorIdentificacao;
    }
  }
}