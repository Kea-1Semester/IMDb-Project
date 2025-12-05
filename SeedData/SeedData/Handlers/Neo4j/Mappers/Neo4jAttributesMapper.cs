using Neo4j.Driver; // <- vigtigt
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;

namespace SeedData.Handlers.Neo4j.Mappers
{
    public static partial class Neo4JAttributesMapper
    {
        public static Task UpsertAttributes(IEnumerable<AttributesEntity> items, int batchSize = 1000)
            => Neo4JMapper.WithWriteSession(session => UpsertAttributes(session, items, batchSize));

        public static async Task UpsertAttributes(IAsyncSession session, IEnumerable<AttributesEntity> items, int batchSize)
        {
            const string cypher = @"
UNWIND $rows AS row
MERGE (a:Attributes { AttributeId: row.AttributeId })
SET   a.Attribute = row.Attribute";

            foreach (var chunk in Neo4JMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(a => new { AttributeId = a.AttributeId.ToString(), a.Attribute }).ToArray();
                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}