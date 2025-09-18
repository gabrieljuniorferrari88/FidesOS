using FidesOS.Comunicacao.Requisicoes.Usuario;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.AlterarSenha;

public interface IAlterarSenhaUsuarioCasoDeUso
{
  Task Execute(RequisicaoAlterarSenhaUsuarioJson request);
}
