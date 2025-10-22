using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SeedData.Handlers;
using EfCoreModelsLib.Models.Mysql;

namespace SeedData;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Env.TraversePath().Load();
        await SeedData();


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

        var optionsBuilder = new DbContextOptionsBuilder<ImdbContext>()
            .UseMySql(
                Env.GetString("ConnectionString"),
                await ServerVersion.AutoDetectAsync(Env.GetString("ConnectionString"))
            )
            .EnableDetailedErrors();

        using (var _context = new ImdbContext(optionsBuilder.Options))
        {
            Console.WriteLine("Ensuring the database exists...");

            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
            }

            Console.WriteLine("Ran database migrations.");

            var titleIdsDict = await TitleBasicsHandler.SeedTitleBasics(_context, titleBasicPath, 100000);
            var personIdsDict = await AddPersonToDb.AddPerson(_context, nameBasicPath, 100000, titleIdsDict);
            await AddCrewToDb.AddCrew(_context, titleCrewPath, 5000, titleIdsDict, personIdsDict);
            await AddEpisode.AddEpisodes(_context, titleEpisodePath, 50000, titleIdsDict);
            await AddActor.AddActorToDb(_context, titlePrincipalsPath, titleIdsDict, personIdsDict);
            await AddRating.AddRatingToDb(_context, titleRatingsPath, titleIdsDict);
            await AddAkas.AddAkasToDb(_context, titleAkasPath, 50000, titleIdsDict);

        }

        Console.WriteLine("Program Completed");

    }

    private static async Task MigrateToMongoDb()
    {

    }
    private static async Task MigrateToNeo4J()
    {

    }
}