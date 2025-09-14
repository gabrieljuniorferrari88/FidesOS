using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Registrar;

public interface IRegistrarUsuarioCasoDeUso
{
  Task<RespostaUsuarioRegistradoJson> Execute(RequisicaoRegistrarUsuarioJson request);
}

