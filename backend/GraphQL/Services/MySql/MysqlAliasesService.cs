using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using GraphQL.Repos.Mysql;

namespace GraphQL.Services.Mysql
{
    public interface IMysqlAliasesService
    {
        IQueryable<Aliases> GetMysqlAliases();
        Task<Aliases?> GetMysqlAlias(Guid id);
        Task<Aliases> CreateMysqlAlias(AliasesDto alias);
        Task<Aliases?> AddAliasToMovie(Guid aliasId, Guid movieId);
        Task<Aliases?> RemoveAliasFromMovie(Guid aliasId, Guid movieId);
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

        public Task<Aliases?> AddAliasToMovie(Guid aliasId, Guid movieId)
        {
            throw new NotImplementedException();
        }

        public Task<Aliases> CreateMysqlAlias(AliasesDto alias)
        {
            throw new NotImplementedException();
        }

        public Task<Aliases> DeleteMysqlAlias(Guid aliasId)
        {
            throw new NotImplementedException();
        }

        public Task<Aliases?> GetMysqlAlias(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Aliases> GetMysqlAliases()
        {
            throw new NotImplementedException();
        }

        public Task<Aliases?> RemoveAliasFromMovie(Guid aliasId, Guid movieId)
        {
            throw new NotImplementedException();
        }
    }
}