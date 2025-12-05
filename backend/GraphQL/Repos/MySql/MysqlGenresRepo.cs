using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repos.Mysql
{
    public interface IMysqlGenresRepo
    {
        IQueryable<Genres> GetMySqlGenres();
        Task<Genres> CreateMySqlGenre(Genres genre);
        Task<Genres> DeleteMySqlGenre(Genres genre);
        Task SaveChanges();
    }

    public class MysqlGenresRepo : IMysqlGenresRepo, IAsyncDisposable
    {
        private readonly ImdbContext _context;

        public MysqlGenresRepo(IDbContextFactory<ImdbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }


        /* ---------- Queries ---------- */
        public IQueryable<Genres> GetMySqlGenres()
        {
            return _context.Genres.AsQueryable();
        }
        

        /* ---------- MUTATIONS ---------- */
        public async Task<Genres> CreateMySqlGenre(Genres genre)
        {
            _context.Genres.Add(genre);
            await SaveChanges();
            return genre;
        }

        public async Task<Genres> DeleteMySqlGenre(Genres genre)
        {
            _context.Remove(genre);
            await SaveChanges();
            return genre;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}