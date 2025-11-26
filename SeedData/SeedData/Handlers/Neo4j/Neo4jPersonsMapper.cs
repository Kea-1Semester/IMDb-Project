using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j
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

            Foreach (charId IN row.CharacterIds |
                MERGE (ch:Characters { CharacterId: charId })
                MERGE (p)-[:PLAYED_AS]->(ch))

            Foreach (knownForId IN row.KnownForIds |
                MERGE (ti:Titles { TitleId: knownForId })
                MERGE (p)-[:KNOWN_FOR]->(ti))

            Foreach (directedId IN row.DirectedIds |
                MERGE (ti:Titles { TitleId: directedId })
                MERGE (p)-[:DIRECTED]->(ti))
            
            Foreach (wroteId IN row.WroteIds |
                MERGE (ti:Titles { TitleId: wroteId })
                MERGE (p)-[:WROTE]->(ti))

            Foreach (actedInAsId IN row.ActedInAsIds |
                MERGE (ti:Titles { TitleId: actedInAsId })
                MERGE (p)-[:ACTED_IN_AS]->(ti)
                SET p.ActedInAsIds = row.ActedInAsIds
            )";

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
                                  ?? Array.Empty<string>(),
                    DirectedIds = p.Directed?.Select(x => x.TitleId.ToString()).Distinct().ToArray()
                                  ?? Array.Empty<string>(),
                    WroteIds = p.Wrote?.Select(x => x.TitleId.ToString()).Distinct().ToArray()
                                ?? Array.Empty<string>(),
                    ActedInAsIds = p.ActedInAs?
                    .Select(x => new { TitleId = x.PlayedIn.ToString(), role = x.CharacterName }).Distinct().ToArray()
                }).ToArray();

                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}