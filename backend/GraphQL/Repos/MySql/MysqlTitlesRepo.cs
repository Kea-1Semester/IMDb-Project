using System.Threading.Tasks;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repos.Mysql
{
    public interface IMysqlTitlesRepo
    {
        IQueryable<Titles> GetMySqlTitles();
        Task<Titles> AddMySqlTitle(Titles title);
        Task<Titles> UpdateMysqlTitle(Titles title);
        Task<Titles> DeleteMysqlTitle(Titles title);
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

        public async Task<Titles> AddMySqlTitle(Titles title)
        {
            await _context.Titles.AddAsync(title);
            await _context.SaveChangesAsync();
            return title;
        }

        public async Task<Titles> UpdateMysqlTitle(Titles title)
        {
            _context.Titles.Update(title);
            await _context.SaveChangesAsync();
            return title;
        }

        public async Task<Titles> DeleteMysqlTitle(Titles title)
        {
            _context.Titles.Remove(title);
            await _context.SaveChangesAsync();
            return title;
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}