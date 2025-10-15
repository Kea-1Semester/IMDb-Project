using SeedData.Models;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SeedData
{
    static class Program
    {
        static void Main(string[] args)
        {
            Env.TraversePath().Load();

            var dbContextOptions = new DbContextOptionsBuilder<ImdbContext>()
                .UseMySql(
                    Env.GetString("ConnectionString"), 
                    ServerVersion.AutoDetect(Env.GetString("ConnectionString"))
                )
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ImdbContext(dbContextOptions))
            {
            }

            Console.WriteLine("Data seeding completed.");
        }
    }
}