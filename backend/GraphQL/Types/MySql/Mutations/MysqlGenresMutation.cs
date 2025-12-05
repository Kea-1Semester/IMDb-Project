using System.ComponentModel.DataAnnotations;
using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services.Mysql;
using HotChocolate.Authorization;

namespace GraphQL.Types.Mysql.Mutations;

[MutationType]
public static class MysqlGenresMutation
{
    [Error(typeof(ValidationException))]
    public static async Task<Genres> CreateMysqlGenre([Service] IMysqlGenresService genresService, GenresDto genre)
    {
        return await genresService.CreateMysqlGenre(genre);
    }

    //Add Genre to Movie
    [Error(typeof(ValidationException))]
    public static async Task<Genres?> AddMysqlGenreToMovie([Service] IMysqlGenresService genresService, Guid genreId, Guid movieId)
    {
        return await genresService.AddGenreToMovie(genreId, movieId);
    }

    //Remove Genre from Movie
    [Error(typeof(ValidationException))]
    public static async Task<Genres?> RemoveMysqlGenreFromMovie([Service] IMysqlGenresService genresService, Guid genreId, Guid titleId)
    {
        return await genresService.RemoveGenreFromMovie(genreId, titleId);
    }

    [Error(typeof(ValidationException))]
    public static async Task<Genres> DeleteMysqlGenre([Service] IMysqlGenresService genresService, Guid id)
    {
        return await genresService.DeleteMysqlGenre(id);
    }
}
