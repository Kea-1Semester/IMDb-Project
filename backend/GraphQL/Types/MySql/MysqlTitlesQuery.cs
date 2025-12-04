using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services.Mysql;
using HotChocolate.Authorization;

namespace GraphQL.Types;

[QueryType]
public static class MysqlTitlesQuery
{
    [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Titles> GetMysqlTitles([Service] IMysqlTitlesService titlesService)
    {
        return titlesService.GetMysqlTitles();
    }
}
