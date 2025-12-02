using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SeedData.DbConnection;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData.Handlers.Neo4j.Migrators
{
    public static class AliasesNeo4JMigrator
    {
        private static AliasesEntity MapAliasesEntity(Aliases src)
        {
            var aliasEntity = new AliasesEntity
            {
                AliasId = src.AliasId,
                Region   = src.Region,
                Language = src.Language,
                IsOriginalTitle = src.IsOriginalTitle,
                Title    = src.Title
            };

            if (src.AttributesAttribute != null)
            {
                aliasEntity.HasAttributes = src.AttributesAttribute
                    .Where(attr => attr != null)
                    .Select(attr => new AttributesEntity
                    {
                        AttributeId = attr!.AttributeId,
                        Attribute        = attr.Attribute
                    })
                    .ToList();
            }

            if (src.TypesType != null)
            {
                aliasEntity.HasTypes = src.TypesType
                    .Where(t => t != null)
                    .Select(t => new TypesEntity
                    {
                        TypeId = t!.TypeId,
                        Type = t.Type
                    })
                    .ToList();
            }

            return aliasEntity;
        }

        public static async Task MigrateAliasesToNeo4j(
            int pageSize = 1000,
            int pages = 0   // 0 = kør til der ikke er flere rækker
        )
        {
            await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

            var pageIndex = 0;
            while (pages == 0 || pageIndex < pages)
            {
                var batchFromMySql = mysqlContext.Aliases
                    .Include(a => a.AttributesAttribute)
                    .Include(a => a.TypesType)
                    .AsNoTracking()
                    .OrderBy(a => a.AliasId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsEnumerable()
                    .Select(MapAliasesEntity)
                    .ToList();

                if (batchFromMySql.Count == 0)
                {
                    Console.WriteLine("No more Aliases found, stopping.");
                    break;
                }

                await Neo4JAliasesMapper.UpsertAliases(batchFromMySql, batchSize: 1000);

                Console.WriteLine(
                    $"Migrated page {pageIndex + 1} with {batchFromMySql.Count} Aliases to Neo4j...");

                pageIndex++;
            }

            Console.WriteLine("✅ Aliases migration to Neo4j done.");
        }
    }
}