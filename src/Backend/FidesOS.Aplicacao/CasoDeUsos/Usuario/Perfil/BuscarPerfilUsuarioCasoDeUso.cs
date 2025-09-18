using FidesOS.Comunicacao.Respostas.Usuario;
using FidesOS.Dominio.Servicos.UsuarioLogado;
using Mapster;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Perfil;

public class BuscarPerfilUsuarioCasoDeUso : IBuscarPerfilUsuarioCasoDeUso
{
  private readonly IUsuarioLogado _usuarioLogado;

  public BuscarPerfilUsuarioCasoDeUso(IUsuarioLogado usuarioLogado) => _usuarioLogado = usuarioLogado;

  public async Task<RespostaUsuarioPerfilJson> Execute()
  {
    var usuario = await _usuarioLogado.Get();

    return usuario.Adapt<RespostaUsuarioPerfilJson>();
  }
}
