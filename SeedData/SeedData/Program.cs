using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SeedData.Handlers;
//using SeedData.Handlers;
using SeedData.Models;

namespace SeedData
{
    static class Program
    {
        static void Main(string[] args)
        {
            Env.TraversePath().Load();

            string? projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            string dataFolder = Path.Combine(projectRoot!, "data");
            string titleBasicPath = Path.Combine(dataFolder, "title.basics.tsv");
            string titleRatingsPath = Path.Combine(dataFolder, "title.ratings.tsv");
            string nameBasicPath = Path.Combine(dataFolder, "name.basics.tsv");
            string titleCrewPath = Path.Combine(dataFolder, "title.crew.tsv");
            string titleEpisodePath = Path.Combine(dataFolder, "title.episode.tsv");
            string titlePrincipalsPath = Path.Combine(dataFolder, "principals.tsv");

            var dbContextOptions = new DbContextOptionsBuilder<ImdbContext>().UseMySql(Env.GetString("ConnectionString"), ServerVersion.AutoDetect(Env.GetString("ConnectionString"))).Options;
            Console.WriteLine("Starting data seeding...");

            using (var context = new ImdbContext(dbContextOptions))
            {

                //TitleBasicsHandler.SeedTitleBasics(context, titleBasicPath, 100000);
                //AddPersonToDb.AddPerson(context, nameBasicPath, 100000, titlePrincipalsPath);
                //AddCrewToDb.AddCrew(context, titleCrewPath, 50000);
                //AddEpisode.AddEpisodes(context, titleEpisodePath, 50000);
                //AddActor.AddActorToDb(context, titlePrincipalsPath);

                var actionMovies = context.Titles
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






            }

            Console.WriteLine("Data seeding completed.");

        }


    }
}
