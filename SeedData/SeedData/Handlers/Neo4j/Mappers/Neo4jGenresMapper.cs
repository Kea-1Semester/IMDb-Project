using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j.Mappers
{
    public static partial class Neo4JGenresMapper
    {
        public static Task UpsertGenres(IEnumerable<GenresEntity> items, int batchSize = 1000)
            => Neo4JMapper.WithWriteSession(session => UpsertGenres(session, items, batchSize));

        public static async Task UpsertGenres(IAsyncSession session, IEnumerable<GenresEntity> items, int batchSize)
        {
            const string cypher = @"
            UNWIND $rows AS row
            MERGE (g:Genres { GenreId: row.GenreId })
            SET   g.Genre = row.Genre
            ";

            foreach (var chunk in Neo4JMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(g => new { GenreId = g.GenreId.ToString(), g.Genre }).ToArray();
                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}