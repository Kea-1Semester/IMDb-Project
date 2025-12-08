using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using GraphQL.Repos.Mysql;

namespace GraphQL.Services.Mysql
{
    public interface IMysqlAliasesService
    {
        Task<Aliases> CreateMysqlAlias(AliasesDto alias);
        Task<Aliases> UpdateMysqlAlias(AliasesDto alias, Guid aliasId);
        Task<Aliases> DeleteMysqlAlias(Guid aliasId);
    }

    public class MysqlAliasesService : IMysqlAliasesService
    {
        private readonly IMysqlAliasesRepo _aliasesRepo;
        private readonly IMysqlTitlesRepo _titlesRepo;

        public MysqlAliasesService(IMysqlAliasesRepo aliasesRepo, IMysqlTitlesRepo titlesRepo)
        {
            _aliasesRepo = aliasesRepo;
            _titlesRepo = titlesRepo;
        }

        /* ---------- MUTATIONS ---------- */
        public async Task<Aliases> CreateMysqlAlias(AliasesDto alias)
        {
            alias.Validate();
            Aliases newAlias = new Aliases
            {
                AliasId = Guid.NewGuid(),
                Region = alias.Region,
                Language = alias.Language,
                IsOriginalTitle = alias.IsOriginalTitle,
                Title = alias.Title,
                TitleId = alias.TitleId
            };
            return await _aliasesRepo.CreateMysqlAlias(newAlias);
        }

        public async Task<Aliases> UpdateMysqlAlias(AliasesDto alias, Guid aliasId)
        {
            alias.Validate();
            Aliases? aliasToUpdate = await _aliasesRepo.GetMysqlAliases().FirstOrDefaultAsync(a => a.AliasId == aliasId);
            if (aliasToUpdate == null)
            {
                throw new GraphQLException(new Error("Alias not found","ALIAS_NOT_FOUNT"));
            }
            aliasToUpdate.Region = alias.Region;
            aliasToUpdate.Language = alias.Language;
            aliasToUpdate.IsOriginalTitle = alias.IsOriginalTitle;

            return await _aliasesRepo.UpdateMysqlAliases(aliasToUpdate);
        }

        public async Task<Aliases> DeleteMysqlAlias(Guid aliasId)
        {
            Aliases? aliasToDelete = await _aliasesRepo.GetMysqlAliases().FirstOrDefaultAsync(a => a.AliasId == aliasId);
            if (aliasToDelete == null)
            {
                throw new GraphQLException(new Error("Alias not found", "ALIAS_NOT_FOUND"));
            }
            return await _aliasesRepo.DeleteMysqlAlias(aliasToDelete);
        }

        
    }
}