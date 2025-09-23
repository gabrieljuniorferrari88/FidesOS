using FidesOS.Dominio.Entidades;

namespace FidesOS.Dominio.Repositorios.OrdensDeServicos;

public interface IRepositorioLeituraOrdemDeServico
{
  Task<List<OrdemDeServico>> ListarOSPorGestor(Guid gestorIdentificacao, int pagina, int itensPorPagina);

  Task<int> ContarOSPorGestor(Guid gestorIdentificacao);

  Task<OrdemDeServico?> BuscarPorId(Guid osId);

  Task<OrdemDeServico?> BuscarDetalhadaPorId(Guid osId);
}