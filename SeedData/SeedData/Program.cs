using DotNetEnv;
using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.SchemaValidator;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
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
        //ConnectionStringDocker
        // await TitleMongoDbMapper.MigrateToMongoDb(40000, 100);
        //await MigrateToNeo4J();
    }

    private static async Task SeedData()
    {
        var projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;


        // MySqlConnection default connection is connected to the cloud sql instance
        await using (var context = MySqlSettings.MySqlConnection("ConnectionStringDocker"))
        {
            Console.WriteLine("Ensuring the database exists...");

            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
            }

            Console.WriteLine("Ran database migrations.");


            if (Environment.GetEnvironmentVariable("SEED_SAMPLE_DATA") == "true")
            {
                var mySqlDataFolder = Path.Combine(projectRoot!, "mySql");
                await context.Database.ExecuteSqlRawAsync(Path.Combine(mySqlDataFolder, "mysqldump.sql"));
                Console.WriteLine("Seeded Sample Data into MySQL Database.");
                
            }
            else
            {
                var dataFolder = Path.Combine(projectRoot!, "data");
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
                await AddEpisode.AddEpisodes(context, titleEpisodePath, 50000, titleIdsDict);
                await AddActor.AddActorToDb(context, titlePrincipalsPath, titleIdsDict, personIdsDict);
                await AddRating.AddRatingToDb(context, titleRatingsPath, titleIdsDict);
                await AddAkas.AddAkasToDb(context, titleAkasPath, 50000, titleIdsDict);
            }
        }

        Console.WriteLine("Program Completed");
    }


    private static async Task MigrateToNeo4J()
    {
    }
}


