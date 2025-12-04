using EfCoreModelsLib.DTO;

namespace GraphQL.Interface
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        T? GetById(Guid id);
        T? Add(T entity);
        T? Update(int id, T entity);
        T? Delete(Guid id);
    }
}

