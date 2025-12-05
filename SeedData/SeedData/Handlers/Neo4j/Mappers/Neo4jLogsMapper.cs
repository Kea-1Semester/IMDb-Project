using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j.Mappers
{
    public static partial class Neo4JLogsMapper
    {
        public static Task UpsertLogs(IEnumerable<LogsEntity> items, int batchSize = 1000)
            => Neo4JMapper.WithWriteSession(session => UpsertLogs(session, items, batchSize));

        public static async Task UpsertLogs(IAsyncSession session, IEnumerable<LogsEntity> items, int batchSize)
        {
            const string cypher = @"
            UNWIND $rows AS row
            MERGE (l:Logs { LogId: row.LogId })
            SET   l.TableName    = row.TableName,
                  l.Command      = row.Command,
                  l.NewValue  = row.NewValue,
                  l.OldValue  = row.OldValue,
                  l.ExecutedBy    = row.ExecutedBy,
                  l.ExecutedAt    = row.ExecutedAt
                  ";

            foreach (var chunk in Neo4JMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(l => new
                {
                    LogId = l.LogId.ToString(),
                    l.TableName,
                    l.Command,
                    l.NewValue,
                    l.OldValue,
                    l.ExecutedBy,
                    ExecutedAt = l.ExecutedAt.ToString("o")
                }).ToArray();

                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}