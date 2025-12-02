using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SeedData.DbConnection;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData.Handlers.Neo4j.Migrators
{
    public static class ProfessionsNeo4JMigrator
    {
        private static ProfessionsEntity MapProfessionsEntity(Professions src)
        {
            return new ProfessionsEntity
            {
                ProfessionId = src.ProfessionId,
                Profession   = src.Profession
            };
        }

        public static async Task MigrateProfessionsToNeo4j(
            int pageSize = 1000,
            int pages = 0   // 0 = kør til der ikke er flere rækker
        )
        {
            await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

            var pageIndex = 0;
            while (pages == 0 || pageIndex < pages)
            {
                var batchFromMySql = mysqlContext.Professions
                    .AsNoTracking()
                    .OrderBy(a => a.ProfessionId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsEnumerable()
                    .Select(MapProfessionsEntity)
                    .ToList();

                if (batchFromMySql.Count == 0)
                {
                    Console.WriteLine("No more Professions found, stopping.");
                    break;
                }

                await Neo4JProfessionsMapper.UpsertProfessions(batchFromMySql, batchSize: 1000);

                Console.WriteLine(
                    $"Migrated page {pageIndex + 1} with {batchFromMySql.Count} Professions to Neo4j...");

                pageIndex++;
            }

            Console.WriteLine("✅ Professions migration to Neo4j done.");
        }
    }
}