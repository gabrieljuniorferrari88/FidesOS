using FidesOS.Dominio.Entidades;

namespace FidesOS.Dominio.Repositorios.OrdensDeServicos.DetalhesProducao;

public interface IRepositorioEscritaDetalheProducao
{
  Task AddAsync(DetalheProducao detalhe);
}