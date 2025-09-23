using FidesOS.Dominio.Entidades;

namespace FidesOS.Dominio.Repositorios.OrdensDeServicos.AlocarTrabalhador;

public interface IRepositorioEscritaAlocacao
{
  Task AddAsync(AlocacaoTrabalhador alocacao);
}
