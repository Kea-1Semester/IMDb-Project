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

            var titleIdsDict = await TitleBasicsHandler.SeedTitleBasics(context, titleBasicPath, 100000);
            var personIdsDict = await AddPersonToDb.AddPerson(context, nameBasicPath, 100000, titleIdsDict);
            await AddCrewToDb.AddCrew(context, titleCrewPath, 50000, titleIdsDict, personIdsDict);
            await AddEpisode.AddEpisodes(context, titleEpisodePath, 50000, titleIdsDict);
            await AddActor.AddActorToDb(context, titlePrincipalsPath, titleIdsDict, personIdsDict);
            await AddRating.AddRatingToDb(context, titleRatingsPath, titleIdsDict);
            await AddAkas.AddAkasToDb(context, titleAkasPath, 50000, titleIdsDict);

        }

        Console.WriteLine("Program Completed");

    }



    private static async Task MigrateToMongoDb()
    {
        var mongoDbData = new List<TitleMongoDb>();

        // 1. Read from MySQL and join data that match the MongoDB Schema
        await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData("ConnectionStringDocker");


        mongoDbData.AddRange(await TitleMongoDbMapper.ListTitleMongoData(mysqlContext));



        /*var episodes = await mysqlContext.Episodes
            .AsNoTracking()
            .ToListAsync();

        var joinEpisodesWithTiles = (
            from e in mysqlContext.Episodes
            join p in mysqlContext.Titles on e.TitleIdParent equals p.TitleId
            join c in mysqlContext.Titles on e.TitleIdChild equals c.TitleId
            select new
            {
                TitleIdParent = e.TitleIdParent,
                TitleIdChild = e.TitleIdChild,
                ParentTitle = p.PrimaryTitle,
                ChildTitle = c.PrimaryTitle
            }
        ).ToList();*/

        /*var titlesFromMysql = await mysqlContext.Titles
            .Include(t => t.GenresGenre)
            .Include(t => t.Aliases)
            .Include(t => t.Comments)
            .Include(t => t.Ratings)
            .Include(t => t.EpisodesTitleIdParentNavigation)
                 .ThenInclude(e => e.TitleIdChildNavigation)
            .Include(t => t.Actors)
                .ThenInclude(a => a.PersonsPerson)
            .Include(t => t.Directors)
                .ThenInclude(d => d.PersonsPerson)
            .Include(r => r.Writers)
                .ThenInclude(w => w.PersonsPerson)
            .AsNoTracking()
            .Take(10000)
            .ToListAsync();


        var titlesList = titlesFromMysql
            .Where(t =>
                t.Aliases.Any() &&
                t.Ratings != null &&
                t.Actors.Any() &&
                t.Directors.Any() &&
                t.Writers.Any()
                )
            .Take(100)
            .ToList();

        var listWithEpisodes = titlesFromMysql
            .Where(t =>
                (
                t.EpisodesTitleIdParentNavigation.Count != 0 ||
                t.EpisodesTitleIdChildNavigation.Count != 0
                )
            )
            .ToList();

        // combine both lists
        var bothLists = titlesList.Union(listWithEpisodes).Distinct()
            .Select(TitleMongoDbMapper.MapTitleMongoDb)
            .Distinct()
            .ToList();


        mongoDbData.AddRange(bothLists);*/

        // 2. Validate / Create MongoDB and Collections

        MongoSchemaInitializer.EnsureCollectionSchema(
        Env.GetString("MongoDbConnectionStr"),
        "imdb-mongo-db",
        "Titles",
        TitlesValidator.GetSchema());

        // 3. Migrate Data to MongoDB
        await using var contextMongo = MongoDbSettings.MongoDbConnection();
        Console.WriteLine("Ensuring the MongoDB database exists...");
        try
        {
            await  contextMongo.Database.EnsureDeletedAsync();
            await contextMongo.Database.EnsureCreatedAsync();
            Console.WriteLine("MongoDB database ensured.");


        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
        }

        await contextMongo.Titles.AddRangeAsync(mongoDbData);
        await contextMongo.SaveChangesAsync();

    }
    private static async Task MigrateToNeo4J()
    {


    }
}
