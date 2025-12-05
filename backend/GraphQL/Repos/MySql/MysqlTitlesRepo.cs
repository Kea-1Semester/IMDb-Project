using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repos.Mysql
{
    public interface IMysqlTitlesRepo
    {
        IQueryable<Titles> GetMySqlTitles();
        Task<Titles?> GetMySqlTitle(Guid id);
    }

    public class MysqlTitlesRepo : IMysqlTitlesRepo, IAsyncDisposable
    {
        private readonly ImdbContext _context;

        public MysqlTitlesRepo(IDbContextFactory<ImdbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }

        public IQueryable<Titles> GetMySqlTitles()
        {
            return _context.Titles.AsQueryable();
        }

        public async Task<Titles?> GetMySqlTitle(Guid id)
        {
            return await _context.Titles.FirstOrDefaultAsync(t => t.TitleId == id);
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}