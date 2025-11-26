using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j
{
    public static partial class Neo4jProfessionsMapper
    {
        public static Task UpsertProfessions(IEnumerable<ProfessionsEntity> items, int batchSize = 1000)
            => Neo4jMapper.WithWriteSession(session => UpsertProfessions(session, items, batchSize));

        public static async Task UpsertProfessions(IAsyncSession session, IEnumerable<ProfessionsEntity> items, int batchSize)
        {
            const string cypher = @"
            UNWIND $rows AS row
            MERGE (pr:Professions { ProfessionId: row.ProfessionId })
            SET   pr.Profession = row.Profession";

            foreach (var chunk in Neo4jMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(pr => new { ProfessionId = pr.ProfessionId.ToString(), pr.Profession }).ToArray();
                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}