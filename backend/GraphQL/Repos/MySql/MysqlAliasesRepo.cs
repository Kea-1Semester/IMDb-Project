using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repos.Mysql
{
    public interface IMysqlAliasesRepo
    {
        IQueryable<Aliases> GetMySqlAliases();
        Task<Aliases?> GetMySqlAlias(Guid id);
        Task<Aliases> CreateMySqlAlias(Aliases alias);
        Task<Aliases?> AddAliasToMovie(Guid aliasId, Guid movieId);
        Task<Aliases?> RemoveAliasFromMovie(Guid aliasId, Guid movieId);
        Task<Aliases> DeleteMySqlAlias(Aliases alias);
    }

    public class MysqlAliasesRepo : IMysqlAliasesRepo, IAsyncDisposable
    {
        private readonly ImdbContext _context;

        public MysqlAliasesRepo(IDbContextFactory<ImdbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }

        /* ---------- Queries ---------- */
        public Task<Aliases?> AddAliasToMovie(Guid aliasId, Guid movieId)
        {
            throw new NotImplementedException();
        }

        public Task<Aliases> CreateMySqlAlias(Aliases alias)
        {
            throw new NotImplementedException();
        }

        /* ---------- MUTATIONS ---------- */
        public Task<Aliases> DeleteMySqlAlias(Aliases alias)
        {
            throw new NotImplementedException();
        }

        public Task<Aliases?> GetMySqlAlias(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Aliases> GetMySqlAliases()
        {
            throw new NotImplementedException();
        }

        public Task<Aliases?> RemoveAliasFromMovie(Guid aliasId, Guid movieId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}