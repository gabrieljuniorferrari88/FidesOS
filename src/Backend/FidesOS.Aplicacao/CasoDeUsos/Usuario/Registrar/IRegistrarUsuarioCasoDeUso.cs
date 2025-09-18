using FidesOS.Comunicacao.Requisicoes.Usuario;
using FidesOS.Comunicacao.Respostas.Usuario;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Registrar;

public interface IRegistrarUsuarioCasoDeUso
{
  Task<RespostaUsuarioRegistradoJson> Execute(RequisicaoRegistrarUsuarioJson request);
}

