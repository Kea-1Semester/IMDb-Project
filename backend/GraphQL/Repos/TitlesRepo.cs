using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repos
{
    public interface ITitlesRepo
    {
        IQueryable<Titles> GetMySqlTitles();
    }

    public class TitlesRepo : ITitlesRepo, IAsyncDisposable
    {
        private readonly ImdbContext _context;

        public TitlesRepo(IDbContextFactory<ImdbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }

        public IQueryable<Titles> GetMySqlTitles()
        {
            return _context.Titles.AsQueryable();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}