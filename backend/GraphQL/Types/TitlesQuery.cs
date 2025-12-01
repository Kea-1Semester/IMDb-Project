using EfCoreModelsLib.Models.Mysql;
using GraphQL.Services;
using HotChocolate.Authorization;

namespace GraphQL.Types;

[QueryType]
public static class TitlesQuery
{
    [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [Authorize]
    public static IQueryable<Titles> GetTitles([Service] ITitlesService titlesService)
    {
        return titlesService.GetTitles();
    }
}
