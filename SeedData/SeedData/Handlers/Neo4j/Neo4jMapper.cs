using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;
using EfCoreModelsLib.models.Neo4J.Neo4JModels;
using EfCoreModelsLib.models.Neo4J.Handler;
using DotNetEnv;
using MongoDB.Driver.Core.Misc;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;

namespace SeedData.Handlers.Neo4j
{
    public static class Neo4jMapper
    {
        public static async Task MigrateToNeo4j(
            IEnumerable<AttributesEntity> source,
            int batchSize = 1000)
        {
            var uri = Env.GetString("NEO4J_URI");
            var user = Env.GetString("NEO4J_USER");
            var pass = Env.GetString("NEO4J_PASSWORD");

            await Neo4jSchemaInitializer.EnsureConstraintsAsync(uri, user, pass);

            var driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, pass));
            await using var session = driver.AsyncSession();

            var buffer = new List<AttributesEntity>(batchSize);

            foreach (var attribute in source)
            {
                buffer.Add(attribute);

                if (buffer.Count >= batchSize)
                {
                    await UpsertBatch(session, buffer);
                    buffer.Clear();
                }
            }

            if (buffer.Count > 0)
            {
                await UpsertBatch(session, buffer);
            }

            await session.CloseAsync();
            await driver.CloseAsync();
        }

        public static async Task UpsertBatch(IAsyncSession session, List<AttributesEntity> attributes)
        {
            const string cypher = @"
            UNWIND $rows AS row
            MERGE (a:Attributes { AttributeId: row.AttributeId })
            SET a.Attribute = row.Attribute";

            var param = new
            {
                rows = attributes.ConvertAll(a => new
                {
                    AttributeId = a.AttributeId.ToString(),
                    Attribute   = a.Attribute
                })
            };

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(cypher, param);
            });
        }
    }
}