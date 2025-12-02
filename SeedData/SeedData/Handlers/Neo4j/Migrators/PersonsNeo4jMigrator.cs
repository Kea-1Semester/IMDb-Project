using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SeedData.DbConnection;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData.Handlers.Neo4j.Migrators
{
    public static class PersonsNeo4jMigrator
    {
        private static PersonsEntity MapPersonsEntity(Persons src)
        {
            var personEntity = new PersonsEntity
            {
                PersonId = src.PersonId,
                Name   = src.Name,
                BirthYear = src.BirthYear,
                EndYear = src.EndYear
            };

            if (src.Professions != null)
            {
                personEntity.HasProfessions = src.Professions
                    .Where(prof => prof != null)
                    .Select(prof => new ProfessionsEntity
                    {
                        ProfessionId = prof!.ProfessionId,
                        Profession = prof.Profession
                    })
                    .ToList();
            }

            if (src.KnownFor != null)
            {
                personEntity.KnownFor = src.KnownFor
                    .Where(title => title != null)
                    .Select(title => new TitlesEntity
                    {
                        TitleId = title!.TitlesTitleId,
                    })
                    .ToList();
            }

            return personEntity;
        }

        public static async Task MigratePersonsToNeo4j(
            int pageSize = 1000,
            int pages = 0   // 0 = kør til der ikke er flere rækker
        )
        {
            await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

            var pageIndex = 0;
            while (pages == 0 || pageIndex < pages)
            {
                var batchFromMySql = mysqlContext.Persons
                    .Include(p => p.Professions)
                    .Include(p => p.KnownFor)
                    .AsNoTracking()
                    .OrderBy(a => a.PersonId)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .AsEnumerable()
                    .Select(MapPersonsEntity)
                    .ToList();

                if (batchFromMySql.Count == 0)
                {
                    Console.WriteLine("No more Persons found, stopping.");
                    break;
                }

                await Neo4jPersonsMapper.UpsertPersons(batchFromMySql, batchSize: 1000);

                Console.WriteLine(
                    $"Migrated page {pageIndex + 1} with {batchFromMySql.Count} Persons to Neo4j...");

                pageIndex++;
            }

            Console.WriteLine("✅ Persons migration to Neo4j done.");
        }
    }
}