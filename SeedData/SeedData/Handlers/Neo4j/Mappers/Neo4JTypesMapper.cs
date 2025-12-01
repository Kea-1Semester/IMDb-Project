
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j.Mappers
{
    public static partial class Neo4jTypesMapper
    {
        public static Task UpsertTypes(IEnumerable<TypesEntity> items, int batchSize = 1000)
            => Neo4jMapper.WithWriteSession(session => UpsertTypes(session, items, batchSize));

        public static async Task UpsertTypes(IAsyncSession session, IEnumerable<TypesEntity> items, int batchSize)
        {
            const string cypher = @"
UNWIND $rows AS row
MERGE (t:Types { TypeId: row.TypeId })
SET   t.Type = row.Type";

            foreach (var chunk in Neo4jMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(t => new { TypeId = t.TypeId.ToString(), t.Type }).ToArray();
                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}