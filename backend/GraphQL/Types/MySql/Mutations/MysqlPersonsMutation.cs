using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services.Mysql;
using HotChocolate.Authorization;

namespace GraphQL.Types.Mysql.Mutations;

[MutationType]
public static class MysqlPersonsMutation
{
    public static async Task<Persons> CreateMysqlPerson(PersonsDto person, [Service] IMysqlPersonsService personsService)
    {
        return await personsService.CreateMysqlPerson(person);
    }

    public static async Task<Persons> UpdateMysqlPerson(Guid id, PersonsDto person, [Service] IMysqlPersonsService personsService)
    {
        return await personsService.UpdateMysqlPerson(person, id);
    }

    public static async Task<Persons> DeleteMysqlPerson(Guid id, [Service] IMysqlPersonsService personsService)
    {
        return await personsService.DeleteMysqlPerson(id);
    }
}
