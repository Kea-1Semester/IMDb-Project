using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.ObjDbContext;
using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using GraphQL.Auth0;
using GraphQL.Interface;
using GraphQL.Services.Interface;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services;

[QueryType]
public static class TitlesQuery
{
    [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Titles> GetTitles([Service] ITitlesService<Titles, ImdbContext> titlesService)
    {
        return titlesService.GetAll();
    }

    [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 100)]
    //[UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<TitleMongoDb> GetMongoTitles([Service] ITitlesService<TitleMongoDb, ImdbContextMongoDb> mongoService)
    {
        return mongoService.GetAll().AsQueryable().AsNoTracking();
    }


    [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 100)]
    //[UseProjection]
    [UseFiltering]
    [UseSorting]
    public async static Task<IEnumerable<TitlesEntity>> GetNeo4JTitles(
        [Service] Neo4jTitlesService neoService)
    {
        return await neoService.GetAllAsync();
    }


}
