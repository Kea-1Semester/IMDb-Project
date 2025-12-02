using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SeedData.DbConnection;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData.Handlers.Neo4j.Migrators
{
    public static class TitlesNeo4jMigrator
    {
        private static TitlesEntity MapTitlesEntity(Titles src)
        {
            var titleEntity = new TitlesEntity
            {
                TitleId = src.TitleId,
                TitleType   = src.TitleType,
                PrimaryTitle = src.PrimaryTitle,
                OriginalTitle = src.OriginalTitle,
                IsAdult = src.IsAdult,
                StartYear = src.StartYear,
                EndYear = src.EndYear,
                RuntimeMinutes = src.RuntimeMinutes
            };

            if (src.GenresGenre != null)
            {
                titleEntity.HasGenres = src.GenresGenre
                    .Where(g => g != null)
                    .Select(g => new GenresEntity
                    {
                        GenreId = g!.GenreId,
                        Genre = g.Genre
                    })
                    .ToList();
            }

            if (src.Ratings != null)
            {
                titleEntity.HasRating = src.Ratings
                .Where(r => r != null)
                .Select(r => new RatingsEntity
                {
                    RatingId = r!.RatingId,
                    AverageRating = r.AverageRating,
                    NumVotes = r.NumVotes
                }).FirstOrDefault();
            }

            if (src.Comments != null)
            {
                titleEntity.HasComments = src.Comments
                .Where(c => c != null)
                .Select(c => new CommentsEntity
                {
                    CommentId = c!.CommentId,
                    Comment = c.Comment
                })
                .ToList();
            }

            if (src.Aliases != null)
            {
                titleEntity.HasAliases = src.Aliases
                .Where(a => a != null)
                .Select(a => new AliasesEntity
                {
                    AliasId = a!.AliasId,
                })
                .ToList();
            }

            if (src.Directors != null)
            {
                titleEntity.Director = src.Directors
                .Where(p => p != null)
                .Select(p => new PersonsEntity
                {
                    PersonId = p.DirectorsId,
                })
                .ToList();
            }

            if (src.Writers != null)
            {
                titleEntity.Writer = src.Writers
                .Where(p => p != null)
                .Select(p => new PersonsEntity
                {
                    PersonId = p.WritersId,
                })
                .ToList();
            }

            if (src.Actors != null)
            {
                titleEntity.Actor = src.Actors
                .Where(a => a != null)
                .Select(a => new ActedInRelationship
                {
                    Person = new PersonsEntity
                    {
                        PersonId = a.PersonsPersonId,
                        
                    },
                    Title = titleEntity,
                    Role = string.IsNullOrWhiteSpace(a.Role) || a.Role == "\\N" ? "" : a.Role
                })
                .ToList();
            }

            return titleEntity;
        }

        public static async Task MigrateTitlesToNeo4j(
            int pageSize = 1000,
            int pages = 0   // 0 = kør til der ikke er flere rækker
        )
        {
            await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

            var pageIndex = 0;
            while (pages == 0 || pageIndex < pages)
            {
                var batchFromMySql = mysqlContext.Titles
                    .Include(t => t.GenresGenre)
                    .Include(t => t.Ratings)
                    .Include(t => t.Comments)
                    .Include(t => t.Aliases)
                    .Include(t => t.Directors)
                    .Include(t => t.Writers)
                    .Include(t => t.Actors)
                    .AsNoTracking()
                    .OrderBy(a => a.TitleId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsEnumerable()
                    .Select(MapTitlesEntity)
                    .ToList();

                if (batchFromMySql.Count == 0)
                {
                    Console.WriteLine("No more Titles found, stopping.");
                    break;
                }

                await Neo4jTitlesMapper.UpsertTitles(batchFromMySql, batchSize: 1000);

                Console.WriteLine(
                    $"Migrated page {pageIndex + 1} with {batchFromMySql.Count} Titles to Neo4j...");

                pageIndex++;
            }

            Console.WriteLine("✅ Titles migration to Neo4j done.");
        }
    }
}