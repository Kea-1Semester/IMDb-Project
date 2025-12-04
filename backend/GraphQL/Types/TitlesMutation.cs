using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.ObjDbContext;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Services.Interface;

namespace GraphQL.Types
{
    [MutationType]
    public static class TitlesMutation
    {
        public static Titles? AddTitleMySql(
            TitlesDto input,
            [Service] ITitlesService<Titles, ImdbContext> titlesService)
        {
            input.Validate();
            return titlesService.Add(MapToTitlesEntity(input));

        }

        public static TitleMongoDb? AddTitleMongoDb(
            TitlesDto input,
            [Service] ITitlesService<TitleMongoDb, ImdbContextMongoDb> titlesService)
        {
            input.Validate();
            return titlesService.Add(MapToTitleMongoDbEntity(input));
        }

        public static Titles? DeleteTitleMySql(
            Guid titleId,
            [Service] ITitlesService<Titles, ImdbContext> titlesService)
        {
            var titles = titlesService.Delete(titleId);
            return titles;
        }

        public static TitleMongoDb? DeleteTitleMongoDb(
            Guid titleId,
            [Service] ITitlesService<TitleMongoDb, ImdbContextMongoDb> titlesService)
        {
            var titles = titlesService.Delete(titleId);
            return titles;
        }


        //TODO: Move this to its own Mapper class later
        #region Move this to its own Mapper class later
        // TitlesDto Mapper to Titles entity
        private static Titles MapToTitlesEntity(TitlesDto dto)
        {
            return new Titles
            {
                TitleId = Guid.NewGuid(),
                TitleType = dto.TitleType,
                PrimaryTitle = dto.PrimaryTitle,
                OriginalTitle = dto.OriginalTitle,
                IsAdult = dto.IsAdult,
                StartYear = dto.StartYear,
                EndYear = dto.EndYear,
                RuntimeMinutes = dto.RuntimeMinutes
            };
        }

        // TitlesDto Mapper to TitleMongoDb entity
        private static TitleMongoDb MapToTitleMongoDbEntity(TitlesDto dto)
        {
            return new TitleMongoDb
            {
                TitleId = Guid.NewGuid(),
                TitleType = dto.TitleType,
                PrimaryTitle = dto.PrimaryTitle,
                OriginalTitle = dto.OriginalTitle,
                IsAdult = dto.IsAdult,
                StartYear = dto.StartYear,
                EndYear = dto.EndYear,
                RuntimeMinutes = dto.RuntimeMinutes
            };
        }

        #endregion



    }
}
