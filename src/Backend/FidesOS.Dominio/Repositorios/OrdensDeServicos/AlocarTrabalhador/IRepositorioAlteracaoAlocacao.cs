using FidesOS.Dominio.Entidades;

namespace FidesOS.Dominio.Repositorios.OrdensDeServicos.AlocarTrabalhador;

public interface IRepositorioAlteracaoAlocacao
{
  Task<AlocacaoTrabalhador?> BuscarPorId(Guid alocacaoId);
}