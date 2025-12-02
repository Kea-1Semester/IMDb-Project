using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SeedData.DbConnection;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData.Handlers.Neo4j.Migrators
{
    public static class AttributesNeo4JMigrator
    {
        private static AttributesEntity MapAttributesEntity(Attributes src)
        {
            return new AttributesEntity
            {
                AttributeId = src.AttributeId,
                Attribute   = src.Attribute
            };
        }

        public static async Task MigrateAttributesToNeo4j(
            int pageSize = 1000,
            int pages = 0   // 0 = kør til der ikke er flere rækker
        )
        {
            await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

            var pageIndex = 0;
            while (pages == 0 || pageIndex < pages)
            {
                var batchFromMySql = mysqlContext.Attributes
                    .AsNoTracking()
                    .OrderBy(a => a.AttributeId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsEnumerable()
                    .Select(MapAttributesEntity)
                    .ToList();

                if (batchFromMySql.Count == 0)
                {
                    Console.WriteLine("No more attributes found, stopping.");
                    break;
                }

                await Neo4JAttributesMapper.UpsertAttributes(batchFromMySql, batchSize: 1000);

                Console.WriteLine(
                    $"Migrated page {pageIndex + 1} with {batchFromMySql.Count} attributes to Neo4j...");

                pageIndex++;
            }

            Console.WriteLine("✅ Attributes migration to Neo4j done.");
        }
    }
}