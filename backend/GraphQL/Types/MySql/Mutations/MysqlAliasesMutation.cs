using System.ComponentModel.DataAnnotations;
using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services.Mysql;
using HotChocolate.Authorization;

namespace GraphQL.Types.Mysql.Mutations;

[MutationType]
public static class MysqlAliasesMutation
{
    [Error(typeof(ValidationException))]
    public static async Task<Aliases> CreateMysqlAlias([Service] IMysqlAliasesService aliasesService, AliasesDto alias)
        => await aliasesService.CreateMysqlAlias(alias);
    
    [Error(typeof(ValidationException))]
    public static async Task<Aliases> UpdateMysqlAlias([Service] IMysqlAliasesService aliasesService, AliasesDto alias, Guid aliasId)
        => await aliasesService.UpdateMysqlAlias(alias, aliasId);

    [Error(typeof(ValidationException))]
    public static async Task<Aliases> DeleteMysqlAlias([Service] IMysqlAliasesService aliasesService, Guid aliasId)
        => await aliasesService.DeleteMysqlAlias(aliasId);
    
}