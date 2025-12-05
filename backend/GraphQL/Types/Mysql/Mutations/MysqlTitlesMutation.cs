using System.Threading.Tasks;
using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services.Mysql;
using HotChocolate.Authorization;

namespace GraphQL.Types.Mysql.Queries;

[MutationType]
public static class MysqlTitlesMutation
{
    [Error(typeof(ArgumentException))]
    [Error(typeof(InvalidOperationException))]
    public static async Task<Titles> CreateMysqlTitle([Service] IMysqlTitlesService titlesService, TitlesDto title) => await titlesService.CreateMysqlTitle(title);

    [Error(typeof(ArgumentException))]
    [Error(typeof(InvalidOperationException))]
    public static async Task<Titles> UpdateMysqlTitle([Service] IMysqlTitlesService titlesService, TitlesDto title, Guid id) => await titlesService.UpdateMysqlTitle(title, id);

    public static async Task<Titles> DeleteMysqlTitle([Service] IMysqlTitlesService titlesService, Guid id) => await titlesService.DeleteMysqlTitle(id);
}
