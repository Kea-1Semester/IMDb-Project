using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Neo4j.Driver;

namespace SeedData.Handlers.Neo4j.Mappers
{
    public static partial class Neo4jTitlesMapper
    {
        public static Task UpsertTitles(IEnumerable<TitlesEntity> items, int batchSize = 1000)
            => Neo4jMapper.WithWriteSession(session => UpsertTitles(session, items, batchSize));

        public static async Task UpsertTitles(IAsyncSession session, IEnumerable<TitlesEntity> items, int batchSize)
        {
            const string cypher = @"
            UNWIND $rows AS row
            MERGE (ti:Titles { TitleId: row.TitleId })
            SET   ti.TitleType       = row.TitleType,
                ti.PrimaryTitle    = row.PrimaryTitle,
                ti.OriginalTitle   = row.OriginalTitle,
                ti.IsAdult         = row.IsAdult,
                ti.StartYear       = row.StartYear,
                ti.EndYear         = row.EndYear,
                ti.RuntimeMinutes  = row.RuntimeMinutes

                FOREACH (epId IN row.EpisodeIds |
                    MERGE (ep:Titles { TitleId: epId })
                    MERGE (ti)-[:EPISODES]->(ep)
                )

                FOREACH (seriesId IN row.SeriesIds |
                    MERGE (s:Titles { TitleId: seriesId })
                    MERGE (ti)-[:SERIES]->(s)
                )

                FOREACH (genresId IN row.GenreIds |
                    MERGE (g:Genres { GenreId: genresId })
                    MERGE (ti)-[:HAS_GENRES]->(g)
                )

                FOREACH (ratingId IN row.RatingIds |
                    MERGE (r:Ratings { RatingId: ratingId })
                    MERGE (ti)-[:HAS_RATING]->(r)
                )

                FOREACH (commentId IN row.CommentIds |
                    MERGE (c:Comments { CommentId: commentId })
                    MERGE (ti)-[:HAS_COMMENTS]->(c)
                )

                FOREACH (aliasId IN row.AliasIds |
                    MERGE (al:Aliases { AliasId: aliasId })
                    MERGE (ti)-[:HAS_ALIASES]->(al)
                )
                
                FOREACH (actor IN row.PlayedAsBy |
                    MERGE (p:Persons { PersonId: actor.personId })
                    MERGE (ti)-[r:ACTED]->(p)
                    SET r.role = actor.role
                )

                FOREACH (dirId IN row.DirectorIds |
                    MERGE (p:Persons { PersonId: dirId })
                    MERGE (ti)-[:DIRECTOR]->(p)
                )

                FOREACH (writerId IN row.WriterIds |
                    MERGE (p:Persons { PersonId: writerId })
                    MERGE (ti)-[:WRITER]->(p)
                )
            ";

            foreach (var chunk in Neo4jMapper.Chunk(items, batchSize))
            {
                var rows = chunk.Select(ti => new
                {
                    TitleId = ti.TitleId.ToString(),
                    ti.TitleType,
                    ti.PrimaryTitle,
                    ti.OriginalTitle,
                    ti.IsAdult,
                    ti.StartYear,
                    ti.EndYear,
                    ti.RuntimeMinutes,

                    // Title-To-Title Relationships
                    EpisodeIds = ti.Episodes?
                        .Select(x => x.TitleId.ToString())
                        .Distinct()
                        .ToArray() ?? Array.Empty<string>(),

                    SeriesIds = ti.Series != null 
                        ? new[] { ti.Series.TitleId.ToString() } 
                        : Array.Empty<string>(),


                    // Genres, Ratings, Comments, Aliases Relationships
                    GenreIds = ti.HasGenres?
                        .Select(x => x.GenreId.ToString())
                        .Distinct()
                        .ToArray() ?? Array.Empty<string>(),

                    RatingIds = ti.HasRating != null 
                        ? new[] { ti.HasRating.RatingId.ToString() } 
                        : Array.Empty<string>(),

                    CommentIds = ti.HasComments?
                        .Select(x => x.CommentId.ToString())
                        .Distinct()
                        .ToArray() ?? Array.Empty<string>(),

                    AliasIds = ti.HasAliases?
                        .Select(x => x.AliasId.ToString())
                        .Distinct()
                        .ToArray() ?? Array.Empty<string>(),
                    

                    // Persons Relationships 
                    WriterIds = ti.Writer?
                        .Select(x => x.PersonId.ToString())
                        .Distinct()
                        .ToArray() ?? Array.Empty<string>(),

                    DirectorIds = ti.Director?
                        .Select(x => x.PersonId.ToString())
                        .Distinct()
                        .ToArray() ?? Array.Empty<string>(),

                    PlayedAsBy = ti.Actor?
                        .Select(x => new { personId = x.Person.PersonId.ToString(), role = x.Role })
                        .Distinct()
                        .ToArray() ?? Array.Empty<object>()

                }).ToArray();

                await session.ExecuteWriteAsync(tx => tx.RunAsync(cypher, new { rows }));
            }
        }
    }
}