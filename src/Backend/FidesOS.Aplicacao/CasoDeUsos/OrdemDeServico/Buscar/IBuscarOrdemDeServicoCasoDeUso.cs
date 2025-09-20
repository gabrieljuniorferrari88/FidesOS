using FidesOS.Comunicacao.Respostas.OrdemDeServico;

namespace FidesOS.Aplicacao.CasoDeUsos.OrdemDeServico.Buscar;

public interface IBuscarOrdemDeServicoCasoDeUso
{
  Task<RespostaOrdemDeServicoJson> Execute(Guid osId);
}
