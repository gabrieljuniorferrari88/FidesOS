namespace FidesOS.Dominio.Repositorios.OrdensDeServicos;

public interface IRepositorioEscritaOrdemDeServico
{
  Task AddAsync(Entidades.OrdemDeServico os);
}
