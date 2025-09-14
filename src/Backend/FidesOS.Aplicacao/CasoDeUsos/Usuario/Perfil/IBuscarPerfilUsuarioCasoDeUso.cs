using FidesOS.Comunicacao.Respostas;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Perfil;

public interface IBuscarPerfilUsuarioCasoDeUso
{
  Task<RespostaUsuarioPerfilJson> Execute();
}
