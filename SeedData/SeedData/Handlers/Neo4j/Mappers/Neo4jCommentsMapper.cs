using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j.Mappers
{
    public static partial class Neo4jCommentsMapper
    {
        public static Task UpsertComments(IEnumerable<CommentsEntity> items, int batchSize = 1000)
            => Neo4jMapper.WithWriteSession(session => UpsertComments(session, items, batchSize));

        public static async Task UpsertComments(IAsyncSession session, IEnumerable<CommentsEntity> items, int batchSize)
        {
            const string cypher = @"
            UNWIND $rows AS row
            MERGE (c:Comments { CommentId: row.CommentId })
            SET   c.Comment = row.Comment";

            foreach (var chunk in Neo4jMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(c => new { CommentId = c.CommentId.ToString(), c.Comment }).ToArray();
                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}