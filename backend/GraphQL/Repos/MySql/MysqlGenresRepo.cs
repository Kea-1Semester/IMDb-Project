using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repos.Mysql
{
    public interface IMysqlGenresRepo
    {
        IQueryable<Genres> GetMySqlGenres();
        Task<Genres?> GetMySqlGenre(Guid id);
        Task<Genres> CreateMySqlGenre(Genres genre);
        Task<Genres?> AddGenreToMovie(Guid genreId, Guid titleId);
        Task<Genres?> RemoveGenreFromMovie(Guid genreId, Guid titleId);
        Task<Genres> DeleteMySqlGenre(Genres genre);
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

        public async Task<Genres?> GetMySqlGenre(Guid id)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.GenreId == id);
        }
        

        /* ---------- MUTATIONS ---------- */
        public async Task<Genres> CreateMySqlGenre(Genres genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genres?> AddGenreToMovie(Guid genreId, Guid titleId)
        {
            var titleGenre = await _context.Titles.Include(m => m.GenresGenre).FirstOrDefaultAsync(m => m.TitleId == titleId);
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreId == genreId);

            titleGenre?.GenresGenre.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genres?> RemoveGenreFromMovie(Guid genreId, Guid titleId)
        {
            var titleGenre = await _context.Titles.Include(m => m.GenresGenre).FirstOrDefaultAsync(m => m.TitleId == titleId);
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreId == genreId);

            titleGenre?.GenresGenre.Remove(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genres> DeleteMySqlGenre(Genres genre)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}