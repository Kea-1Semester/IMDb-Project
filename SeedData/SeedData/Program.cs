using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeedData.Handlers;
using SeedData.Models;

namespace SeedData
{
    static class Program
    {
        private const int seed = 123456;

        static async Task Main(string[] args)
        {
            Env.TraversePath().Load();

            var optionsBuilder = new DbContextOptionsBuilder<ImdbContext>()
                .UseMySql(
                    Env.GetString("ConnectionString"),
                    ServerVersion.AutoDetect(Env.GetString("ConnectionString"))
                )
                .UseAsyncSeeding(async (dbContext, _, cancellationToken) =>
                {
                    Console.WriteLine("Initializing seeding of data");
                    
                    await SeedDataHandler.SeedAsync(dbContext, cancellationToken, seed);
                    
                    Console.WriteLine("Finished seeding");
                })
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            using (var _context = new ImdbContext(optionsBuilder.Options))
            {
                Console.WriteLine("Ensuring the database exists...");

                var exists = await _context.Database.EnsureCreatedAsync();

                Console.WriteLine(exists ? "Created the database" : "Database already exists");
            }

            Console.WriteLine("Program Completed");
        }
    }
}