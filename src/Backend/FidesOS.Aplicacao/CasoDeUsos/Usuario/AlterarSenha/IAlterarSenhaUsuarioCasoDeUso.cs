using FidesOS.Comunicacao.Requisicoes;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.AlterarSenha;

public interface IAlterarSenhaUsuarioCasoDeUso
{
  Task Execute(RequisicaoAlterarSenhaUsuarioJson request);
}
