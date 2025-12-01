using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j.Mappers
{
    public static partial class Neo4jAliasesMapper
    {
        public static Task UpsertAliases(IEnumerable<AliasesEntity> items, int batchSize = 1000)
            => Neo4jMapper.WithWriteSession(session => UpsertAliases(session, items, batchSize));

        public static async Task UpsertAliases(IAsyncSession session, IEnumerable<AliasesEntity> items, int batchSize)
        {
            const string cypher = @"
UNWIND $rows AS row
MERGE (al:Aliases { AliasId: row.AliasId })
SET   al.Region          = row.Region,
      al.Language        = row.Language,
      al.IsOriginalTitle = row.IsOriginalTitle,
      al.Title           = row.Title

FOREACH (attId IN row.AttributeIds |
  MERGE (a:Attributes { AttributeId: attId })
  MERGE (al)-[:HAS_ATTRIBUTE]->(a)
)

FOREACH (typeId IN row.TypeIds |
  MERGE (t:Types { TypeId: typeId })
  MERGE (al)-[:HAS_TYPE]->(t)
)";

            foreach (var chunk in Neo4jMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(al => new
                {
                    AliasId = al.AliasId.ToString(),
                    al.Region,
                    al.Language,
                    al.IsOriginalTitle,
                    al.Title,
                    AttributeIds = al.HasAttributes?.Select(x => x.AttributeId.ToString()).Distinct().ToArray()
                                   ?? Array.Empty<string>(),
                    TypeIds = al.HasTypes?.Select(x => x.TypeId.ToString()).Distinct().ToArray()
                               ?? Array.Empty<string>()
                }).ToArray();

                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}