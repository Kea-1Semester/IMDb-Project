using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j.Mappers
{
    public static partial class Neo4jPersonsMapper
    {
        public static Task UpsertPersons(IEnumerable<PersonsEntity> items, int batchSize = 1000)
            => Neo4jMapper.WithWriteSession(session => UpsertPersons(session, items, batchSize));

        public static async Task UpsertPersons(IAsyncSession session, IEnumerable<PersonsEntity> items, int batchSize)
        {
            const string cypher = @"
            UNWIND $rows AS row
            MERGE (p:Persons { PersonId: row.PersonId })
            SET     p.Name       = row.Name,
                    p.BirthYear  = row.BirthYear, 
                    p.EndYear    = row.EndYear

            Foreach (profId IN row.ProfessionIds |
                MERGE (pr:Professions { ProfessionId: profId })
                MERGE (p)-[:HAS_PROFESSION]->(pr))

            Foreach (knownForId IN row.KnownForIds |
                MERGE (ti:Titles { TitleId: knownForId })
                MERGE (p)-[:KNOWN_FOR]->(ti))
            ";

            foreach (var chunk in Neo4jMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(p => new
                {
                    PersonId = p.PersonId.ToString(),
                    p.Name,
                    p.BirthYear,
                    p.EndYear,
                    ProfessionIds = p.HasProfessions?.Select(x => x.ProfessionId.ToString()).Distinct().ToArray()
                                    ?? Array.Empty<string>(),
                    KnownForIds = p.KnownFor?.Select(x => x.TitleId.ToString()).Distinct().ToArray()
                                  ?? Array.Empty<string>()
                }).ToArray();

                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}