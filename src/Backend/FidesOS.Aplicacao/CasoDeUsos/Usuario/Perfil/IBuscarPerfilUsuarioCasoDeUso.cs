using FidesOS.Comunicacao.Respostas.Usuario;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Perfil;

public interface IBuscarPerfilUsuarioCasoDeUso
{
  Task<RespostaUsuarioPerfilJson> Execute();
}
