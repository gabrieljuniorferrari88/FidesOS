using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;

namespace FidesOS.Aplicacao.CasoDeUsos.FazerLogin;

public interface IFazerLoginCasoDeUso
{
  Task<RespostaUsuarioRegistradoJson> Execute(RequisicaoLoginJson request);
}
