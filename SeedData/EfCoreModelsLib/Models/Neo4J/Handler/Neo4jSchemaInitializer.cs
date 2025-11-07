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
            var driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            await using var session = driver.AsyncSession();

            const string constraint =
                "CREATE CONSTRAINT attribute_id_Unique IF NOT EXISTS " +
                "FOR (a:Attributes) REQUIRE a.AttributeId IS UNIQUE";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(constraint);
            });

            await session.CloseAsync();
            await driver.CloseAsync();
        }
    }
}
