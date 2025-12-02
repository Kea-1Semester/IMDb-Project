using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services
{
    public interface ITitlesService
    {
        IQueryable<Titles> GetTitles();
    }

    public class TitlesService : ITitlesService, IAsyncDisposable
    {
        private readonly ImdbContext _context;

        public TitlesService(IDbContextFactory<ImdbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }

        public IQueryable<Titles> GetTitles()
        {
            return _context.Titles.AsQueryable();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}