using FidesOS.Dominio.Entidades;

namespace FidesOS.Dominio.Repositorios.OrdensDeServicos;

public interface IRepositorioAlteracaoOrdemDeServico
{
  Task<OrdemDeServico?> BuscarPorId(Guid osId);
}
