using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j.Mappers
{
    public static partial class Neo4jRatingsMapper
    {
        public static Task UpsertRatings(IEnumerable<RatingsEntity> items, int batchSize = 1000)
            => Neo4jMapper.WithWriteSession(session => UpsertRatings(session, items, batchSize));

        public static async Task UpsertRatings(IAsyncSession session, IEnumerable<RatingsEntity> items, int batchSize)
        {
            const string cypher = @"
            UNWIND $rows AS row
            MERGE (r:Ratings { RatingId: row.RatingId })
            SET   r.AverageRating = row.AverageRating,
                r.NumVotes      = row.NumVotes
            ";

            foreach (var chunk in Neo4jMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(r => new
                {
                    RatingId = r.RatingId.ToString(),
                    r.AverageRating,
                    r.NumVotes
                }).ToArray();

                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}