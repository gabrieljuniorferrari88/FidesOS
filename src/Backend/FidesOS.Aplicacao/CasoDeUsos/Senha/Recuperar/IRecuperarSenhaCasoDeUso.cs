using FidesOS.Comunicacao.Requisicoes.Senha;
using FidesOS.Comunicacao.Respostas.Senha;

namespace FidesOS.Aplicacao.CasoDeUsos.Senha.Recuperar;

public interface IRecuperarSenhaCasoDeUso
{
  Task<RespostaRecuperarSenhaJson> Execute(RequisicaoRecuperarSenhaJson request); 
}
