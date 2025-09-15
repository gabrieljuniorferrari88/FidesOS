using FidesOS.Comunicacao.Requisicoes;
using FidesOS.Comunicacao.Respostas;

namespace FidesOS.Aplicacao.CasoDeUsos.Senha.Recuperar;

public interface IRecuperarSenhaCasoDeUso
{
  Task<RespostaRecuperarSenhaJson> Execute(RequisicaoRecuperarSenhaJson request); 
}
