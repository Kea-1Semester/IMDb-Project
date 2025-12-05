using System.Threading.Tasks;
using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services.Mysql;
using HotChocolate.Authorization;

namespace GraphQL.Types.Mysql.Queries;

[QueryType]
public static class MysqlTitlesQuery
{
    [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Titles> GetMysqlTitles([Service] IMysqlTitlesService titlesService) => titlesService.GetMysqlTitles();

    public static async Task<Titles?> GetMysqlTitle([Service] IMysqlTitlesService titlesService, Guid id)
        => await titlesService.GetMysqlTitle(id);
}
