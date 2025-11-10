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
            await using var driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            await using var session = driver.AsyncSession(o => o.WithDefaultAccessMode(AccessMode.Write));

            var statements = new[]
            {
                // Attributes
                "CREATE CONSTRAINT attributes_id_unique IF NOT EXISTS FOR (a:Attributes) REQUIRE a.AttributeId IS UNIQUE",
                "CREATE CONSTRAINT attributes_attribute_exists IF NOT EXISTS FOR (a:Attributes) REQUIRE a.Attribute IS NOT NULL",

                // Eksempel på andre labels fra dit IMDB-domæne
                "CREATE CONSTRAINT persons_id_unique IF NOT EXISTS FOR (p:Person) REQUIRE p.PersonId IS UNIQUE",
                "CREATE CONSTRAINT titles_id_unique IF NOT EXISTS FOR (t:Title) REQUIRE t.TitleId IS UNIQUE",
                "CREATE CONSTRAINT characters_id_unique IF NOT EXISTS FOR (c:Character) REQUIRE c.CharacterId IS UNIQUE",

                // Relationsegenskaber (hvis du bruger properties på relationer)
                // Fx hvis du fjerner Character-noden og gemmer rollen på relationen:
                "CREATE CONSTRAINT acted_in_role_exists IF NOT EXISTS FOR ()-[r:PLAYED_IN]-() REQUIRE r.role IS NOT NULL"
            };

            await session.ExecuteWriteAsync(async tx =>
            {
                foreach (var stmt in statements)
                    await tx.RunAsync(stmt);
            });
        }
    }
}
