using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GraphQL.Repos.GenericRepo
{
    public class GenericRepo<T, TContext> : IAsyncDisposable, IGenericRepository<T>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericRepo(IDbContextFactory<TContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public T? GetById(Guid id)
        {
            //var entity = _context.Set<T>().Find(id);
            var propertyName = typeof(T).GetProperty("TitleId") != null ? "TitleId" : "title_id";
            var entity  = _context.Set<T>().FirstOrDefault(e => EF.Property<Guid>( e, propertyName) == id);
            return entity;
        }

        public T? Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity) + " cannot be null");
            }
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;

        }

        public T? Update(int id, T entity)
        {
            throw new NotImplementedException();
        }

        public T? Delete(Guid id)
        {
            var obj = GetById(id);
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
            return obj;
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
