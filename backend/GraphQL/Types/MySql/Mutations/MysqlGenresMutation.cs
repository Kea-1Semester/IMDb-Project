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

    //Add Genre to Title
    [Error(typeof(ValidationException))]
    public static async Task<Genres?> AddMysqlGenre([Service] IMysqlGenresService genresService, Guid genreId, Guid titleId)
    {
        return await genresService.AddMysqlGenre(genreId, titleId);
    }

    //Remove Genre from Title
    [Error(typeof(ValidationException))]
    public static async Task<Genres?> RemoveMysqlGenre([Service] IMysqlGenresService genresService, Guid genreId, Guid titleId)
    {
        return await genresService.RemoveMysqlGenre(genreId, titleId);
    }

    [Error(typeof(ValidationException))]
    public static async Task<Genres> DeleteMysqlGenre([Service] IMysqlGenresService genresService, Guid id)
    {
        return await genresService.DeleteMysqlGenre(id);
    }
}
