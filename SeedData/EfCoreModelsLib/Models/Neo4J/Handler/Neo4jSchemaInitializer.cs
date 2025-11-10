using System;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Misc;
using Neo4j.Driver;

namespace EfCoreModelsLib.models.Neo4J.Handler
{
    public static class Neo4jSchemaInitializer
    {
        public static async Task EnsureConstraintsAsync(string uri, string user, string password)
        {
            // ONLY RUNS ON COMMUNITY EDITION

            await using var driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            await using var session = driver.AsyncSession(o => o.WithDefaultAccessMode(AccessMode.Write));

            var statements = new[]
            {
                // Attributes
                "CREATE CONSTRAINT attributes_id_unique IF NOT EXISTS FOR (a:Attributes) REQUIRE a.AttributeId IS UNIQUE",

                // Persons
                "CREATE CONSTRAINT persons_id_unique IF NOT EXISTS FOR (p:Person) REQUIRE p.PersonId IS UNIQUE",

                // Titles
                "CREATE CONSTRAINT titles_id_unique IF NOT EXISTS FOR (t:Title) REQUIRE t.TitleId IS UNIQUE",

                // Characters
                "CREATE CONSTRAINT characters_id_unique IF NOT EXISTS FOR (c:Character) REQUIRE c.CharacterId IS UNIQUE"
            };

            await session.ExecuteWriteAsync(async tx =>
            {
                foreach (var stmt in statements)
                {
                    await tx.RunAsync(stmt);
                }
            });

            Console.WriteLine("✅ Neo4j constraints ensured (Community Edition)");
        }
    }
}
