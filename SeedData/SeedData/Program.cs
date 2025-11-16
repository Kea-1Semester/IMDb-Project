using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SeedData.DbConnection;
using SeedData.Handlers;
using SeedData.Handlers.MongoDb;

namespace SeedData;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Env.TraversePath().Load();
        await SeedData();
        await TitleMongoDbMapper.MigrateToMongoDb(40000, 4);
        //await MigrateToNeo4J();
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
    }
}


