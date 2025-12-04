using EfCoreModelsLib.DTO;
using GraphQL.Repos.GenericRepo;
using GraphQL.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services.MySQL
{
    public class TitlesService<T, TContext>(GenericRepo<T, TContext> repo) : ITitlesService<T, TContext>
        where T : class
        where TContext : DbContext
    {
        public IQueryable<T> GetAll()
        {
            return repo.GetAll();
        }
        // Add entity
        public T Add(T entity)
        {
            repo.Add(entity);
            return entity;
        }
        // delete entity
        public T Delete(Guid id)
        {
            repo.Delete(id);
            return null;
        }

    }
}