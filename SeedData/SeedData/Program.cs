using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SeedData.Handlers;
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

            var dbContextOptions = new DbContextOptionsBuilder<ImdbContext>().UseMySql(Env.GetString("ConnectionString"), ServerVersion.AutoDetect(Env.GetString("ConnectionString"))).Options;
            Console.WriteLine("Starting data seeding...");

            var titleBasics = new List<Title>();

            using (var context = new ImdbContext(dbContextOptions))
            {

                TitleBasicsHandler.SeedTitleBasics(context, titleBasicPath);
                AddPersonToDb.AddPerson(context, nameBasicPath);




            }

            Console.WriteLine("Data seeding completed.");

        }
        //static short ParseYear(string value)
        //{
        //    if (value == "\\N") return 0;
        //    if (short.TryParse(value, out var year) && (year == 0 || (year >= 1901 && year <= 2155)))
        //        return year;
        //    // cant be null represented in db so return 9999
        //    return 0;
        //}

    }
}