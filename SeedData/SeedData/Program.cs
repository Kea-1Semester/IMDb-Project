using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeedData.Handlers;
using SeedData.Models;

namespace SeedData;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Env.TraversePath().Load();

        var projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        var dataFolder = Path.Combine(projectRoot!, "data");
        var titleBasicPath = Path.Combine(dataFolder, "title.basics.tsv");
        var titleRatingsPath = Path.Combine(dataFolder, "title.ratings.tsv");
        var nameBasicPath = Path.Combine(dataFolder, "name.basics.tsv");
        var titleCrewPath = Path.Combine(dataFolder, "title.crew.tsv");
        var titleEpisodePath = Path.Combine(dataFolder, "title.episode.tsv");
        var titlePrincipalsPath = Path.Combine(dataFolder, "principals.tsv");
        var titleAkasPath = Path.Combine(dataFolder, "title.akas.tsv");

        var optionsBuilder = new DbContextOptionsBuilder<ImdbContext>()
            .UseMySql(
                Env.GetString("ConnectionString"),
                await ServerVersion.AutoDetectAsync(Env.GetString("ConnectionString"))
            )
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        using (var _context = new ImdbContext(optionsBuilder.Options))
        {
            Console.WriteLine("Ensuring the database exists...");

            await _context.Database.MigrateAsync();

            Console.WriteLine("Ran database migrations.");

            TitleBasicsHandler.SeedTitleBasics(_context, titleBasicPath, 100000);
            AddPersonToDb.AddPerson(_context, nameBasicPath, 100000);
            AddCrewToDb.AddCrew(_context, titleCrewPath, 5000);
            AddEpisode.AddEpisodes(_context, titleEpisodePath, 50000);
            AddActor.AddActorToDb(_context, titlePrincipalsPath);
            AddRating.AddRatingToDb(_context, titleRatingsPath);
            AddAkas.AddAkasToDb(_context, titleAkasPath, 50000);

            /*var actionMovies = context.Titles
            .Include(t => t.GenresGenres)
            .Where(t => t.GenresGenres.Any(g => g.Genre1 == "Action"))
            .Select(t => new
            {
                t.PrimaryTitle,
                t.OriginalTitle,
                t.StartYear,
                Genre = t.GenresGenres
                    .Where(g => g.Genre1 == "Action")
                    .Select(g => g.Genre1)
                    .FirstOrDefault()
            })
            .Take(100)
            .ToList();

            var table = new ConsoleTablePrinter(new[] { "Primary Title", "Original Title", "Start Year", "Genre" });

            foreach (var movie in actionMovies)
            {
                table.AddRow(new[]
                {
                    movie.PrimaryTitle ?? "",
                    movie.OriginalTitle ?? "",
                    movie.StartYear.ToString(),
                    movie.Genre ?? ""
                });
            }
            table.Print();
            */
        }

        Console.WriteLine("Program Completed");
    }
}