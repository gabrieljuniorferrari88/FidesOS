using FidesOS.Comunicacao.Requisicoes;

namespace FidesOS.Aplicacao.CasoDeUsos.Usuario.Alterar;

public interface IAlterarUsuarioCasoDeUso
{
  Task Execute(RequisicaoAlterarUsuarioJson request);
}

