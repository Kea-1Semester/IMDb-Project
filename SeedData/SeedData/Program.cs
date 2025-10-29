using DotNetEnv;
using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.SchemaValidator;
using Microsoft.EntityFrameworkCore;
using SeedData.DbConnection;
using SeedData.Handlers;

namespace SeedData;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Env.TraversePath().Load();
        //await SeedData();
        await MigrateToMongoDb();
        //await MigrateToNeo4J();
    }

    private static async Task SeedData()
    {
        var projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        var dataFolder = Path.Combine(projectRoot!, "data");
        var titleBasicPath = Path.Combine(dataFolder, "title.basics.tsv");
        var titleRatingsPath = Path.Combine(dataFolder, "title.ratings.tsv");
        var nameBasicPath = Path.Combine(dataFolder, "name.basics.tsv");
        var titleCrewPath = Path.Combine(dataFolder, "title.crew.tsv");
        var titleEpisodePath = Path.Combine(dataFolder, "title.episode.tsv");
        var titlePrincipalsPath = Path.Combine(dataFolder, "title.principals.tsv");
        var titleAkasPath = Path.Combine(dataFolder, "title.akas.tsv");

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

            var titleIdsDict = await TitleBasicsHandler.SeedTitleBasics(context, titleBasicPath, 100000);
            var personIdsDict = await AddPersonToDb.AddPerson(context, nameBasicPath, 100000, titleIdsDict);
            await AddCrewToDb.AddCrew(context, titleCrewPath, 5000, titleIdsDict, personIdsDict);
            await AddEpisode.AddEpisodes(context, titleEpisodePath, 50000, titleIdsDict);
            await AddActor.AddActorToDb(context, titlePrincipalsPath, titleIdsDict, personIdsDict);
            await AddRating.AddRatingToDb(context, titleRatingsPath, titleIdsDict);
            await AddAkas.AddAkasToDb(context, titleAkasPath, 50000, titleIdsDict);

        }

        Console.WriteLine("Program Completed");

    }


    private static async Task MigrateToMongoDb()
    {


        // 1. Read from MySQL and join data that match the MongoDB Schema
        await using var mysqlContext = MySqlSettings.MySqlConnection();
        var titlesFromMysql = await mysqlContext.Titles
            .Include(t => t.Aliases)
            .Include(t => t.Comments)
            .Include(t => t.Ratings)
            .Include(t => t.EpisodesTitleIdParentNavigation)
                .ThenInclude( e => e.TitleIdChildNavigation)
            .Include( t => t.EpisodesTitleIdChildNavigation)
                .ThenInclude( e => e.TitleIdParentNavigation)
            .Include(t => t.Actors)
                .ThenInclude(a => a.PersonsPerson)
            .Include(t => t.Directors)
                .ThenInclude(d => d.PersonsPerson)
            //.Include(t => t.Writers)
            //    .ThenInclude(w => w.PersonsPerson)
            .AsNoTracking()
            .Take(10)
            .ToListAsync();


        //// 2. Validate / Create MongoDB and Collections
        //MongoSchemaInitializer.EnsureCollectionSchema(
        //    connectionUri,
        //    "imdb-mongo-db",
        //    "Titles",
        //    TitlesValidator.GetSchema()
        //);

        //// 3. Migrate Data to MongoDB
        //await using var contextMongo = MongoDbConnection(connectionUri);
        //Console.WriteLine("Ensuring the MongoDB database exists...");
        //try
        //{
        //    await contextMongo.Database.EnsureCreatedAsync();
        //    Console.WriteLine("MongoDB database ensured.");


        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
        //}

        //await contextMongo.Titles.AddRangeAsync(await contextMongo.Titles.ToListAsync());

    }
    private static async Task MigrateToNeo4J()
    {

    }
}