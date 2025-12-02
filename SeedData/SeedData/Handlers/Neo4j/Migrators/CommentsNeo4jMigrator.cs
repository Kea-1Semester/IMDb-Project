using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SeedData.DbConnection;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData.Handlers.Neo4j.Migrators
{
    public static class CommentsNeo4JMigrator
    {
        private static CommentsEntity MapCommentsEntity(Comments src)
        {
            return new CommentsEntity
            {
                CommentId = src.CommentId,
                Comment   = src.Comment
            };
        }

        public static async Task MigrateCommentsToNeo4j(
            int pageSize = 1000,
            int pages = 0   // 0 = kør til der ikke er flere rækker
        )
        {
            await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

            var pageIndex = 0;
            while (pages == 0 || pageIndex < pages)
            {
                var batchFromMySql = mysqlContext.Comments
                    .AsNoTracking()
                    .OrderBy(a => a.CommentId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsEnumerable()
                    .Select(MapCommentsEntity)
                    .ToList();

                if (batchFromMySql.Count == 0)
                {
                    Console.WriteLine("No more Comments found, stopping.");
                    break;
                }

                await Neo4JCommentsMapper.UpsertComments(batchFromMySql, batchSize: 1000);

                Console.WriteLine(
                    $"Migrated page {pageIndex + 1} with {batchFromMySql.Count} Comments to Neo4j...");

                pageIndex++;
            }

            Console.WriteLine("✅ Comments migration to Neo4j done.");
        }
    }
}