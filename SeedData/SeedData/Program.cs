using SeedData.Models;
using DotNetEnv;
using SeedData.Handlers;
using Microsoft.EntityFrameworkCore;

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

            var dbContextOptions = new DbContextOptionsBuilder<ImdbContext>().UseMySql(Env.GetString("ConnectionString"), ServerVersion.AutoDetect(Env.GetString("ConnectionString"))).Options;

            using (var context = new ImdbContext(dbContextOptions))
            {
                TitleBasicsHandler.SeedTitleBasics(context, titleBasicPath);
            }

            Console.WriteLine("Data seeding completed.");
        }
    }
}