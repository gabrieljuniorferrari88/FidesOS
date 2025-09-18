using FidesOS.Comunicacao.Requisicoes.Senha;

namespace FidesOS.Aplicacao.CasoDeUsos.Senha.Resetar;

public interface IResetarSenhaCasoDeUso
{
  Task Execute(RequisicaoResetarSenhaJson request);
}
