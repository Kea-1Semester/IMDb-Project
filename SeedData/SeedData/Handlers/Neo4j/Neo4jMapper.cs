// Neo4jMapper.cs
using DotNetEnv;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j
{
    public static class Neo4jMapper
    {
        public static async Task WithWriteSession(Func<IAsyncSession, Task> action)
        {
            var uri = Environment.GetEnvironmentVariable("NEO4J_URI");
            var user = Environment.GetEnvironmentVariable("NEO4J_USER");
            var password = Environment.GetEnvironmentVariable("NEO4J_PASSWORD");

            await using var driver  = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            await using var session = driver.AsyncSession(o => o.WithDefaultAccessMode(AccessMode.Write));
            await action(session);
        }

        public static IEnumerable<List<T>> Chunk<T>(IEnumerable<T> source, int size)
        {
            var batch = new List<T>(size);
            foreach (var item in source)
            {
                batch.Add(item);
                if (batch.Count == size)
                {
                    yield return batch;
                    batch = new List<T>(size);
                }
            }
            if (batch.Count > 0)
                yield return batch;
        }

        public sealed class UpsertPayload
        {
            public IEnumerable<AttributesEntity> Attributes { get; init; }
                = Enumerable.Empty<AttributesEntity>();
            public IEnumerable<TypesEntity> Types { get; init; }
                = Enumerable.Empty<TypesEntity>();
            public IEnumerable<AliasesEntity> Aliases { get; init; }
                = Enumerable.Empty<AliasesEntity>();
        }

        public static async Task UpsertAll(UpsertPayload data, int batchSize = 1000)
        {
            await WithWriteSession(async session =>
            {
                // Kør kun hvis der er data i den pågældende samling
                if (data.Attributes.Any()) await Neo4jAttributesMapper.UpsertAttributes(session, data.Attributes, batchSize);
                if (data.Types.Any())      await Neo4jTypesMapper.UpsertTypes(session, data.Types, batchSize);
                if (data.Aliases.Any())    await Neo4jAliasesMapper.UpsertAliases(session, data.Aliases, batchSize);

                // Tilføj flere linjer her når du får nye entiteter
            });
        }
    }
}