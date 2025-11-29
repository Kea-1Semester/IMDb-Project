using System;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Misc;
using Neo4j.Driver;

namespace EfCoreModelsLib.Models.Neo4J.Handler
{
    public static class Neo4JSchemaInitializer
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

                // Aliases
                "CREATE CONSTRAINT aliases_id_unique IF NOT EXISTS FOR (al:Aliases) REQUIRE al.AliasId IS UNIQUE",

                // Types
                "CREATE CONSTRAINT types_id_unique IF NOT EXISTS FOR (t:Types) REQUIRE t.TypeId IS UNIQUE",
                
                // Titles
                "CREATE CONSTRAINT titles_id_unique IF NOT EXISTS FOR (ti:Titles) REQUIRE ti.TitleId IS UNIQUE",

                // Ratings
                "CREATE CONSTRAINT ratings_id_unique IF NOT EXISTS FOR (r:Ratings) REQUIRE r.RatingId IS UNIQUE",

                // Genres
                "CREATE CONSTRAINT genres_id_unique IF NOT EXISTS FOR (g:Genres) REQUIRE g.GenreId IS UNIQUE",

                // Persons
                "CREATE CONSTRAINT persons_id_unique IF NOT EXISTS FOR (p:Persons) REQUIRE p.PersonId IS UNIQUE",

                // Professions
                "CREATE CONSTRAINT professions_id_unique IF NOT EXISTS FOR (pr:Professions) REQUIRE pr.ProfessionId IS UNIQUE",

                // Comments
                "CREATE CONSTRAINT comments_id_unique IF NOT EXISTS FOR (c:Comments) REQUIRE c.CommentId IS UNIQUE",

                // Logs
                "CREATE CONSTRAINT logs_id_unique IF NOT EXISTS FOR (l:Logs) REQUIRE l.LogId IS UNIQUE"
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
