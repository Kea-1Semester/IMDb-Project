using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace GraphQL.Services
{
    public class Neo4jTitlesService
    {
        private readonly IDriver _driver;

        public Neo4jTitlesService(IDriver driver)
        {
            _driver = driver;
        }

        public async Task<List<TitlesEntity>> GetAllAsync()
        {
            await using var session = _driver.AsyncSession();
            var result = await session.RunAsync(@"
            MATCH (t:Titles)
            Where t.PrimaryTitle IS NOT NULL
            RETURN t.TitleId AS TitleId,
                   t.PrimaryTitle AS PrimaryTitle,
                   t.OriginalTitle AS OriginalTitle,
                   t.StartYear AS StartYear,
                   t.EndYear AS EndYear,
                   t.RuntimeMinutes AS RuntimeMinutes,
                   t.TitleType AS TitleType
        ");

            return await result.ToListAsync(record => new TitlesEntity
            {
                TitleId = Guid.Parse(record["TitleId"].As<string>()),
                PrimaryTitle = record["PrimaryTitle"].As<string>(),
                OriginalTitle = record["OriginalTitle"].As<string>(),
                StartYear = (int)record["StartYear"].As<int?>()!,
                EndYear = record["EndYear"].As<int?>(),
                RuntimeMinutes = record["RuntimeMinutes"].As<int?>(),
                TitleType = record["TitleType"].As<string>()
            });
        }
    }
}
