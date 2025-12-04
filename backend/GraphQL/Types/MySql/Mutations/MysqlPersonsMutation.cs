using System.ComponentModel.DataAnnotations;
using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services.Mysql;
using HotChocolate.Authorization;

namespace GraphQL.Types.Mysql.Mutations;

[MutationType]
public static class MysqlPersonsMutation
{
    [Error(typeof(ValidationException))]
    public static async Task<Persons> CreateMysqlPerson(PersonsDto person, [Service] IMysqlPersonsService personsService)
    {
        return await personsService.CreateMysqlPerson(person);
    }

    [Error(typeof(ValidationException))]
    public static async Task<Persons> UpdateMysqlPerson(Guid id, PersonsDto person, [Service] IMysqlPersonsService personsService)
    {
        return await personsService.UpdateMysqlPerson(person, id);
    }

    [Error(typeof(ValidationException))]
    public static async Task<Persons> DeleteMysqlPerson(Guid id, [Service] IMysqlPersonsService personsService)
    {
        return await personsService.DeleteMysqlPerson(id);
    }
}
