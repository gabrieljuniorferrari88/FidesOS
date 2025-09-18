using FidesOS.Comunicacao.Requisicoes.FazerLogin;
using FidesOS.Comunicacao.Respostas.Usuario;

namespace FidesOS.Aplicacao.CasoDeUsos.FazerLogin;

public interface IFazerLoginCasoDeUso
{
  Task<RespostaUsuarioRegistradoJson> Execute(RequisicaoLoginJson request);
}
