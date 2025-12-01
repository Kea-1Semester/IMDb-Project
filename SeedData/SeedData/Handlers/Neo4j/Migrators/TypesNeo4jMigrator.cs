using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SeedData.DbConnection;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData.Handlers.Neo4j.Migrators
{
    public static class TypesNeo4jMigrator
    {
        private static TypesEntity MapTypesEntity(Types src)
        {
            return new TypesEntity
            {
                TypeId = src.TypeId,
                Type   = src.Type
            };
        }

        public static async Task MigrateTypesToNeo4j(
            int pageSize = 1000,
            int pages = 0   // 0 = kør til der ikke er flere rækker
        )
        {
            await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

            var pageIndex = 0;
            while (pages == 0 || pageIndex < pages)
            {
                var batchFromMySql = mysqlContext.Types
                    .AsNoTracking()
                    .OrderBy(a => a.TypeId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsEnumerable()
                    .Select(MapTypesEntity)
                    .ToList();

                if (batchFromMySql.Count == 0)
                {
                    Console.WriteLine("No more Types found, stopping.");
                    break;
                }

                await Neo4jTypesMapper.UpsertTypes(batchFromMySql, batchSize: 1000);

                Console.WriteLine(
                    $"Migrated page {pageIndex + 1} with {batchFromMySql.Count} Types to Neo4j...");

                pageIndex++;
            }

            Console.WriteLine("✅ Types migration to Neo4j done.");
        }
    }
}