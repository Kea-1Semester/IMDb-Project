using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.MongoDb.SupportClasses;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repos.Mysql
{
    public interface IMysqlAliasesRepo
    {
        
        IQueryable<Aliases> GetMysqlAliases();
        Task<Aliases> CreateMysqlAlias(Aliases alias);
        Task SaveChanges();
    }

    public class MysqlAliasesRepo : IMysqlAliasesRepo, IAsyncDisposable
    {
        private readonly ImdbContext _context;

        public MysqlAliasesRepo(IDbContextFactory<ImdbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }

        public IQueryable<Aliases> GetMysqlAliases()
        {
            return _context.Aliases.AsQueryable();
        }

        /* ---------- MUTATIONS ---------- */

        public async Task<Aliases> CreateMysqlAlias(Aliases alias)
        {
            _context.Aliases.Add(alias);
            await SaveChanges();
            return alias;
        }

        // UPDATE FUNCTION NEEDED
        public async Task<Aliases> UpdateMysqlAliases(Aliases alias)
        {
            _context.Aliases.Update(alias);
            await SaveChanges();
            return alias;
        }

        public async Task<Aliases> DeleteMysqlAlias(Aliases alias)
        {
            _context.Remove(alias);
            await SaveChanges();
            return alias;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}