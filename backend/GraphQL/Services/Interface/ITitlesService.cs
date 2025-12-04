using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services.Interface
{
    public interface ITitlesService<T, TContext>
        where T : class
        where TContext : DbContext
    {
        IQueryable<T> GetAll();
        T? Add(T entity);
        T? Delete(Guid id);
    }
}
