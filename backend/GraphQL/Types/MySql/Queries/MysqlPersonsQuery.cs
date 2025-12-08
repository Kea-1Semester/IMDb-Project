using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services.Mysql;
using HotChocolate.Authorization;

namespace GraphQL.Types.Mysql.Queries;

[QueryType]
public static class MysqlPersonsQuery
{
    [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Persons> GetMysqlPersons([Service] IMysqlPersonsService personsService) 
        => personsService.GetMysqlPersons();

    public static async Task<Persons?> GetMysqlPerson([Service] IMysqlPersonsService personsService, Guid id) 
        => await personsService.GetMysqlPerson(id);
}