using DotNetEnv;
using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.SchemaValidator;
using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using SeedData.DbConnection;
using SeedData.Handlers;
using SeedData.Handlers.MongoDb;
using SeedData.Handlers.Neo4j;

namespace SeedData;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Env.TraversePath().Load();
        //await SeedData();
        //ConnectionStringDocker
        //await TitleMongoDbMapper.MigrateToMongoDb(40000, 100);
        await MigrateToNeo4J();
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
        await using (var context = MySqlSettings.MySqlConnection("ConnectionStringAiven"))
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


    private static async Task MigrateToNeo4J()
    {
        Env.TraversePath().Load();

        // 1) Schema (Community-safe UNIQUE constraints for alle labels du bruger)
        await EfCoreModelsLib.Models.Neo4J.Handler.Neo4jSchemaInitializer.EnsureConstraintsAsync(
            Env.GetString("NEO4J_URI"),
            Env.GetString("NEO4J_USER"),
            Env.GetString("NEO4J_PASSWORD"));

        // 2) Eksempeldata (erstat evt. med dine data fra MySQL)
        var attrColor = new AttributesEntity { AttributeId = Guid.NewGuid(), Attribute = "Color" };
        var typeMovie = new TypesEntity       { TypeId = Guid.NewGuid(),     Type      = "movie" };

        var alias = new AliasesEntity
        {
            AliasId = Guid.NewGuid(),
            Region = "DK",
            Language = "da",
            IsOriginalTitle = false,
            Title = "Den danske titel",
            HasAttributes = new() { attrColor },
            HasTypes      = new() { typeMovie }
        };

        // 3) Kør ALT i én call
        var payload = new Neo4jMapper.UpsertPayload
        {
            Attributes = new[] { attrColor },
            Types      = new[] { typeMovie },
            Aliases    = new[] { alias }
        };

        await Neo4jMapper.UpsertAll(payload, batchSize: 1000);

        Console.WriteLine("✅ Neo4j upsert complete (Attributes, Types, Aliases).");
    
    }
}


