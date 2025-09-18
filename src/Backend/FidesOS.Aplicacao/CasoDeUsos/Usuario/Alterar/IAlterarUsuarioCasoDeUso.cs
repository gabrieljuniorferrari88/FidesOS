using FidesOS.Comunicacao.Requisicoes.Usuario;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Alterar;

public interface IAlterarUsuarioCasoDeUso
{
  Task Execute(RequisicaoAlterarUsuarioJson request);
}

