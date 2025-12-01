using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SeedData.DbConnection;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData.Handlers.Neo4j.Migrators
{
    public static class RatingsNeo4jMigrator
    {
        private static RatingsEntity MapRatingsEntity(Ratings src)
        {
            return new RatingsEntity
            {
                RatingId = src.RatingId,
                AverageRating   = src.AverageRating,
                NumVotes = src.NumVotes
            };
        }

        public static async Task MigrateRatingsToNeo4j(
            int pageSize = 1000,
            int pages = 0   // 0 = kør til der ikke er flere rækker
        )
        {
            await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

            var pageIndex = 0;
            while (pages == 0 || pageIndex < pages)
            {
                var batchFromMySql = mysqlContext.Ratings
                    .AsNoTracking()
                    .OrderBy(a => a.RatingId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsEnumerable()
                    .Select(MapRatingsEntity)
                    .ToList();

                if (batchFromMySql.Count == 0)
                {
                    Console.WriteLine("No more Ratings found, stopping.");
                    break;
                }

                await Neo4jRatingsMapper.UpsertRatings(batchFromMySql, batchSize: 1000);

                Console.WriteLine(
                    $"Migrated page {pageIndex + 1} with {batchFromMySql.Count} Ratings to Neo4j...");

                pageIndex++;
            }

            Console.WriteLine("✅ Ratings migration to Neo4j done.");
        }
    }
}