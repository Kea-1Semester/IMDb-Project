using System.Net.Sockets;
using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Repos.Mysql;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services.Mysql
{
    public interface IMysqlTitlesService
    {
        IQueryable<Titles> GetMysqlTitles();
        Task<Titles> CreateMysqlTitle(TitlesDto titlesDto);
        Task<Titles> UpdateMysqlTitle(TitlesDto titlesDto, Guid id);
        Task<Titles> DeleteMysqlTitle(Guid id);
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

        public async Task<Titles> CreateMysqlTitle(TitlesDto titlesDto)
        {
            titlesDto.Validate();

            Titles newTitle = new Titles
            {
                TitleId = Guid.NewGuid(),
                TitleType = titlesDto.TitleType,
                PrimaryTitle = titlesDto.PrimaryTitle,
                OriginalTitle = titlesDto.OriginalTitle,
                IsAdult = titlesDto.IsAdult,
                StartYear = titlesDto.StartYear,
                EndYear = titlesDto.EndYear,
                RuntimeMinutes = titlesDto.RuntimeMinutes
            };

            return await _titlesRepo.AddMySqlTitle(newTitle);
        }

        public async Task<Titles> UpdateMysqlTitle(TitlesDto titlesDto, Guid id)
        {
            titlesDto.Validate();

            Titles? updateTitle = await _titlesRepo.GetMySqlTitles().FirstOrDefaultAsync(t => t.TitleId == id);
            if (updateTitle is null)
            {
                throw new GraphQLException(new Error("Title was not found", "TITLE_NOT_FOUND"));
            }

            updateTitle.PrimaryTitle = titlesDto.PrimaryTitle;
            updateTitle.OriginalTitle = titlesDto.OriginalTitle;
            updateTitle.TitleType = titlesDto.TitleType;
            updateTitle.IsAdult = titlesDto.IsAdult;
            updateTitle.StartYear = titlesDto.StartYear;
            updateTitle.EndYear = titlesDto.EndYear;

            return await _titlesRepo.UpdateMysqlTitle(updateTitle);
        }

        public async Task<Titles> DeleteMysqlTitle(Guid id)
        {
            Titles? deleteTitle = await _titlesRepo.GetMySqlTitles().FirstOrDefaultAsync(t => t.TitleId == id);
            if (deleteTitle is null)
            {
                throw new GraphQLException(new Error("Title was not found", "TITLE_NOT_FOUND"));
            }

            return await _titlesRepo.DeleteMysqlTitle(deleteTitle);
        }
    }
}