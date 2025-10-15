using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeedData.Models;

namespace SeedData
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            Env.TraversePath().Load();

            var optionsBuilder = new DbContextOptionsBuilder<ImdbContext>()
                .UseMySql(
                    Env.GetString("ConnectionString"),
                    ServerVersion.AutoDetect(Env.GetString("ConnectionString"))
                );

            using (var _context = new ImdbContext(optionsBuilder.Options))
            {
                Console.WriteLine("Ensuring the database exists...");

                var exists = await _context.Database.EnsureCreatedAsync();

                Console.WriteLine(exists ? "Created the database" : "Database already exists");
                Console.WriteLine("Running migration for database...");

                await _context.Database.MigrateAsync();

                Console.WriteLine("Finished trying to migrate");
            }

            Console.WriteLine("Program Completed");
        }
    }
}