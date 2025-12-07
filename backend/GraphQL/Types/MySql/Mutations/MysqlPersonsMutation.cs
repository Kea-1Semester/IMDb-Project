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
    public static async Task<Persons> CreateMysqlPerson([Service] IMysqlPersonsService personsService, PersonsDto person)
    {
        return await personsService.CreateMysqlPerson(person);
    }

    [Error(typeof(ValidationException))]
    public static async Task<Persons> UpdateMysqlPerson([Service] IMysqlPersonsService personsService, PersonsDto person, Guid id)
    {
        return await personsService.UpdateMysqlPerson(person, id);
    }

    [Error(typeof(ValidationException))]
    public static async Task<Persons> DeleteMysqlPerson([Service] IMysqlPersonsService personsService, Guid id)
    {
        return await personsService.DeleteMysqlPerson(id);
    }
}
