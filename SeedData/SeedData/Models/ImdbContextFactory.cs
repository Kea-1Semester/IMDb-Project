using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SeedData.Models
{
    public class ImdbContextFactory : IDesignTimeDbContextFactory<ImdbContext>
    {
        public ImdbContext CreateDbContext(string[] args)
        {
            Env.TraversePath().Load();

            var connectionString = Env.GetString("ConnectionString");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string is not set in environment variables.");

            var optionsBuilder = new DbContextOptionsBuilder<ImdbContext>();
            optionsBuilder.UseMySql(
                Env.GetString("ConnectionString"),
                ServerVersion.AutoDetect(Env.GetString("ConnectionString"))
            );

            return new ImdbContext(optionsBuilder.Options);
        }
    }
}