namespace FidesOS.Dominio.Repositories;

public interface IUnitOfWork
{
  Task Commit();
}