using EfCoreModelsLib.Models.Mysql;
using GraphQL.Repos.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services.Mysql
{
    public interface IMysqlTitlesService
    {
        IQueryable<Titles> GetMysqlTitles();
        Task<Titles?> GetMysqlTitle(Guid id);
    }

    public class MysqlTitlesService : IMysqlTitlesService
    {
        private readonly IMysqlTitlesRepo _titlesRepo;

        public MysqlTitlesService(IMysqlTitlesRepo titlesRepo)
        {
            _titlesRepo = titlesRepo;
        }

        public IQueryable<Titles> GetMysqlTitles()
        {
            return _titlesRepo.GetMySqlTitles();
        }

        public async Task<Titles?> GetMysqlTitle(Guid id)
        {
            return await _titlesRepo.GetMySqlTitle(id);
        }
    }
}