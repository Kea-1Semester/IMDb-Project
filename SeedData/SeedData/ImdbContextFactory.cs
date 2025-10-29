using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EfCoreModelsLib.Models.Mysql;

namespace SeedData
{
    public class ImdbContextFactory : IDesignTimeDbContextFactory<ImdbContext>
    {
        public ImdbContext CreateDbContext(string[] args)
        {
            Env.TraversePath().Load();

            var connectionString = Env.GetString("ConnectionStringDocker");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string is not set in environment variables.");

            var optionsBuilder = new DbContextOptionsBuilder<ImdbContext>();
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                b => b.MigrationsAssembly("SeedData")
            );

            return new ImdbContext(optionsBuilder.Options);
        }
    }
}