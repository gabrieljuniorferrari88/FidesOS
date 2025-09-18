using FidesOS.Comunicacao.Respostas.OrdemDeServico;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Alterar;

public interface IAlterarOrdemDeServicoCasoDeUso
{
  Task Execute(Guid osId, RequisicaoAlterarOrdemDeServicoJson request);
}
