using DotNetEnv;
using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.SchemaValidator;
using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SeedData.DbConnection;
using SeedData.Handlers;
using SeedData.Handlers.MongoDb;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Env.TraversePath().Load();
        await SeedData();
        await TitleMongoDbMapper.MigrateToMongoDb(40000, 4);
        await MigrateToNeo4J();
    }

    private static async Task SeedData()
    {
        var projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;


        // MySqlConnection default connection is connected to the cloud sql instance
        await using (var context = MySqlSettings.MySqlConnection())
        {
            Console.WriteLine("Ensuring the database exists...");

            try
            {
                await context.Database.EnsureDeletedAsync();

                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
            }

            Console.WriteLine("Ran database migrations.");


            if (Environment.GetEnvironmentVariable("SEED_SAMPLE_DATA") == "true")
            {
                var dataRoot = Environment.GetEnvironmentVariable("DATA_ROOT")!;
                var sqlFile = Path.Combine(dataRoot, "mysqldump.sql");
                Console.WriteLine($"Data Root: {dataRoot}");


                await context.Database.ExecuteSqlRawAsync(await File.ReadAllTextAsync(sqlFile));


                // open file advance folder and execute all sql files except Drop.sql
                foreach (var file in Directory.GetFiles(Path.Combine(dataRoot, "advance"), "*.sql"))
                {
                    if (Path.GetFileName(file) == "Drop.sql")
                        continue;
                    Console.WriteLine($"Executing Advanced File: {file}");
                    await context.Database.ExecuteSqlRawAsync(await File.ReadAllTextAsync(file));

                }

                Console.WriteLine("Seeded Sample Data into MySQL Database.");
            }
            else
            {
                var dataFolder = Path.Combine(projectRoot!, "data");
                var mysqlFolder = Path.Combine(projectRoot!, "mysql");

                var titleBasicPath = Path.Combine(dataFolder, "title.basics.tsv");
                var titleRatingsPath = Path.Combine(dataFolder, "title.ratings.tsv");
                var nameBasicPath = Path.Combine(dataFolder, "name.basics.tsv");
                var titleCrewPath = Path.Combine(dataFolder, "title.crew.tsv");
                var titleEpisodePath = Path.Combine(dataFolder, "title.episode.tsv");
                var titlePrincipalsPath = Path.Combine(dataFolder, "title.principals.tsv");
                var titleAkasPath = Path.Combine(dataFolder, "title.akas.tsv");

                var titleIdsDict = await TitleBasicsHandler.SeedTitleBasics(context, titleBasicPath, 100000);
                var personIdsDict = await AddPersonToDb.AddPerson(context, nameBasicPath, 100000, titleIdsDict);
                await AddCrewToDb.AddCrew(context, titleCrewPath, 50000, titleIdsDict, personIdsDict);
                await AddEpisode.AddEpisodes(context, titleEpisodePath, titleIdsDict);
                await AddActor.AddActorToDb(context, titlePrincipalsPath, titleIdsDict, personIdsDict);
                await AddRating.AddRatingToDb(context, titleRatingsPath, titleIdsDict);
                await AddAkas.AddAkasToDb(context, titleAkasPath, 50000, titleIdsDict);

                Console.WriteLine("Seeded Sample Data into MySQL Database.");

                foreach (var file in Directory.GetFiles(Path.Combine(mysqlFolder, "advance"), "*.sql"))
                {
                    if (Path.GetFileName(file) == "Drop.sql")
                        continue;

                    Console.WriteLine($"Executing Advanced File: {file}");

                    await context.Database.ExecuteSqlRawAsync(await File.ReadAllTextAsync(file));

                }
            }
        }

        Console.WriteLine("Program Completed");
    }

    private static async Task MigrateToNeo4J()
    {
        Env.TraversePath().Load();

        // 1) Schema (Community-safe UNIQUE constraints for alle labels du bruger)
        await EfCoreModelsLib.Models.Neo4J.Handler.Neo4JSchemaInitializer.EnsureConstraintsAsync(
            Environment.GetEnvironmentVariable("NEO4J_URI")!,
            Environment.GetEnvironmentVariable("NEO4J_USER")!,
            Environment.GetEnvironmentVariable("NEO4J_PASSWORD")!);

        await Handlers.Neo4j.Migrators.AttributesNeo4JMigrator.MigrateAttributesToNeo4j(1000, 0);
        await Handlers.Neo4j.Migrators.TypesNeo4JMigrator.MigrateTypesToNeo4j(1000, 0);
        await Handlers.Neo4j.Migrators.ProfessionsNeo4JMigrator.MigrateProfessionsToNeo4j(1000, 0);
        await Handlers.Neo4j.Migrators.GenresNeo4JMigrator.MigrateGenresToNeo4j(1000, 0);
        await Handlers.Neo4j.Migrators.RatingsNeo4JMigrator.MigrateRatingsToNeo4j(1000, 0);
        await Handlers.Neo4j.Migrators.CommentsNeo4JMigrator.MigrateCommentsToNeo4j(1000, 0);
        await Handlers.Neo4j.Migrators.AliasesNeo4JMigrator.MigrateAliasesToNeo4j(1000, 0);
        await Handlers.Neo4j.Migrators.PersonsNeo4JMigrator.MigratePersonsToNeo4j(1000, 50);
        await Handlers.Neo4j.Migrators.TitlesNeo4JMigrator.MigrateTitlesToNeo4j(1000, 0);

        Console.WriteLine("✅ migrations to Neo4j done.");
    }
}


