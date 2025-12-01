using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SeedData.DbConnection;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData.Handlers.Neo4j.Migrators
{
    public static class GenresNeo4jMigrator
    {
        private static GenresEntity MapGenresEntity(Genres src)
        {
            return new GenresEntity
            {
                GenreId = src.GenreId,
                Genre   = src.Genre
            };
        }

        public static async Task MigrateGenresToNeo4j(
            int pageSize = 1000,
            int pages = 0   // 0 = kør til der ikke er flere rækker
        )
        {
            await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

            var pageIndex = 0;
            while (pages == 0 || pageIndex < pages)
            {
                var batchFromMySql = mysqlContext.Genres
                    .AsNoTracking()
                    .OrderBy(a => a.GenreId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsEnumerable()
                    .Select(MapGenresEntity)
                    .ToList();

                if (batchFromMySql.Count == 0)
                {
                    Console.WriteLine("No more Genres found, stopping.");
                    break;
                }

                await Neo4jGenresMapper.UpsertGenres(batchFromMySql, batchSize: 1000);

                Console.WriteLine(
                    $"Migrated page {pageIndex + 1} with {batchFromMySql.Count} Genres to Neo4j...");

                pageIndex++;
            }

            Console.WriteLine("✅ Genres migration to Neo4j done.");
        }
    }
}