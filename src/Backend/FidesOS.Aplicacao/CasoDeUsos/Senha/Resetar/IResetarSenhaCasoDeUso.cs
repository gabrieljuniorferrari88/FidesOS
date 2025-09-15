using FidesOS.Comunicacao.Requisicoes;

namespace FidesOS.Aplicacao.CasoDeUsos.Senha.Resetar;

public interface IResetarSenhaCasoDeUso
{
  Task Execute(RequisicaoResetarSenhaJson request);
}
