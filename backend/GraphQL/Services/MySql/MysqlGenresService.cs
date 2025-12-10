using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using GraphQL.Repos.Mysql;
using Neo4j.Driver;

namespace GraphQL.Services.Mysql
{
    public interface IMysqlGenresService
    {
        IQueryable<Genres> GetMysqlGenres();
        Task<Genres> CreateMysqlGenre(GenresDto genre);
        Task<Genres?> AddMysqlGenre(Guid genreId, Guid titleId);
        Task<Genres?> RemoveMysqlGenre(Guid genreId, Guid titleId);
        Task<Genres> DeleteMysqlGenre(Guid genreId);
    }

    public class MysqlGenresService : IMysqlGenresService
    {
        private readonly IMysqlGenresRepo _genresRepo;
        private readonly IMysqlTitlesRepo _titlesRepo;

        public MysqlGenresService(IMysqlGenresRepo genresRepo, IMysqlTitlesRepo titlesRepo)
        {
            _genresRepo = genresRepo;
            _titlesRepo = titlesRepo;
        }

        public IQueryable<Genres> GetMysqlGenres()
        {
            return _genresRepo.GetMySqlGenres();
        }


        /* ---------- Queries ---------- */
        public async Task<Genres> CreateMysqlGenre(GenresDto genre)
        {
            genre.Validate();
            Genres genres = new Genres
            {
                GenreId = Guid.NewGuid(),
                Genre = genre.Genre
            };
            return await _genresRepo.CreateMySqlGenre(genres);
        }


        /* ---------- MUTATIONS ---------- */
        public async Task<Genres?> AddMysqlGenre(Guid genreId, Guid titleId)
        {
            Genres? genre = await _genresRepo.GetMySqlGenres().Include(g => g.TitlesTitle).FirstOrDefaultAsync(g => g.GenreId == genreId);
            if (genre == null)
            {
                throw new GraphQLException(new Error("Genre not found", "GENRE_NOT_FOUND"));
            }

            Titles? title = await _titlesRepo.GetMySqlTitles().FirstOrDefaultAsync(t => t.TitleId == titleId);
            if (title == null)
            {
                throw new GraphQLException(new Error("Title not found", "Title_NOT_FOUND"));
            }
            if (genre.TitlesTitle.Contains(title))
            {
                throw new GraphQLException(new Error("Title already has genre", "TITLE_ALREADY_HAS_GENRE"));
            }

            genre.TitlesTitle.Add(title);
            await _genresRepo.SaveChanges();
            return await _genresRepo.GetMySqlGenres().FirstOrDefaultAsync(g => g.GenreId == genreId);
        }

        public async Task<Genres?> RemoveMysqlGenre(Guid genreId, Guid titleId)
        {
            Genres? genre = await _genresRepo
                .GetMySqlGenres()
                .Include(g => g.TitlesTitle)
                .FirstOrDefaultAsync(g => g.GenreId == genreId);

            if (genre == null)
            {
                throw new GraphQLException(new Error("Genre not found", "GENRE_NOT_FOUND"));
            }

            var title = genre.TitlesTitle.FirstOrDefault(t => t.TitleId == titleId);
            if (title == null)
            {
                throw new GraphQLException(new Error("Title not found", "Title_NOT_FOUND"));
            }

            genre.TitlesTitle.Remove(title);
            await _genresRepo.SaveChanges();
            return await _genresRepo.GetMySqlGenres().FirstOrDefaultAsync(g => g.GenreId == genreId);
        }

        public async Task<Genres> DeleteMysqlGenre(Guid genreId)
        {

            Genres? genreToDelete = await _genresRepo.GetMySqlGenres().FirstOrDefaultAsync(g => g.GenreId == genreId);
            if (genreToDelete == null)
            {
                throw new GraphQLException(new Error("Genre not found", "GENRE_NOT_FOUND"));
            }
            return await _genresRepo.DeleteMySqlGenre(genreToDelete);
        }
    }
}