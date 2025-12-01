using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
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
    [Authorize(AuthPolicy.ReadPolicies)]
    public static IQueryable<Titles> GetTitles([Service] ITitlesService titlesService)
    {
        return titlesService.GetTitles();
    }
}
