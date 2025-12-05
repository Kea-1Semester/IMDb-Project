using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using GraphQL.Repos.Mysql;

namespace GraphQL.Services.Mysql
{
    public interface IMysqlGenresService
    {
        IQueryable<Genres> GetMysqlGenres();
        Task<Genres?> GetMysqlGenre(Guid id);
        Task<Genres> CreateMysqlGenre(GenresDto genre);
        Task<Genres?> AddGenreToMovie(Guid genreId, Guid titleId);
        Task<Genres?> RemoveGenreFromMovie(Guid genreId, Guid titleId);
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
        public async Task<Genres?> GetMysqlGenre(Guid id)
        {
            return await _genresRepo.GetMySqlGenre(id);
        }

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
        public async Task<Genres?> AddGenreToMovie(Guid genreId, Guid titleId)
        {
            Genres? genre =  await _genresRepo.GetMySqlGenre(genreId);
            if (genre == null)
            {
                throw new GraphQLException(new Error("Genre not found", "GENRE_NOT_FOUND"));
            }
            
            Titles? movie = await _titlesRepo.GetMySqlTitle(titleId);
            if (movie == null)
            {
                throw new GraphQLException(new Error("Movie not found", "MOVIE_NOT_FOUND"));
            }
            return await _genresRepo.AddGenreToMovie(genreId, titleId);
        }

        public async Task<Genres?> RemoveGenreFromMovie(Guid genreId, Guid titleId)
        {
            Genres? genre =  await _genresRepo.GetMySqlGenre(genreId);
            if (genre == null)
            {
                throw new GraphQLException(new Error("Genre not found", "GENRE_NOT_FOUND"));
            }
            
            Titles? movie = await _titlesRepo.GetMySqlTitle(titleId);
            if (movie == null)
            {
                throw new GraphQLException(new Error("Movie not found", "MOVIE_NOT_FOUND"));
            }
            return await _genresRepo.RemoveGenreFromMovie(genreId, titleId);
        }

        public async Task<Genres> DeleteMysqlGenre(Guid genreId)
        {
            Genres? genreToDelete = await _genresRepo.GetMySqlGenre(genreId);
            if (genreToDelete == null)
            {
                throw new GraphQLException(new Error("Genre not found", "GENRE_NOT_FOUND"));
            }
            return await _genresRepo.DeleteMySqlGenre(genreToDelete);
        }
    }
}