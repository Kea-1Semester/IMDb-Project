using EfCoreModelsLib.Models.Mysql;
using GraphQL.Repos;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services
{
    public interface ITitlesService
    {
        IQueryable<Titles> GetTitles();
    }

    public class TitlesService : ITitlesService
    {
        private readonly ITitlesRepo _titlesRepo;

        public TitlesService(ITitlesRepo titlesRepo)
        {
            _titlesRepo = titlesRepo;
        }

        public IQueryable<Titles> GetTitles()
        {
            return _titlesRepo.GetMySqlTitles();
        }
    }
}