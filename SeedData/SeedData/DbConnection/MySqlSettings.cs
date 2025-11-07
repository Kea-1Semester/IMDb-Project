using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace SeedData.DbConnection;

public static class MySqlSettings
{
    /// <summary>
    /// This connection is used to apply migrations and seed data to MySql.
    /// This will apply migrations when the context is created with logging enabled.
    /// </summary>
    /// <param name="connectionString"> MySql connection string to apply migrations and seed data to</param>
    /// <returns>ImdbContext </returns>
    public static ImdbContext MySqlConnection(string connectionString = "MySqlConnectionString")
    {
        var mysqlConnectionUri = Environment.GetEnvironmentVariable(connectionString)!;
        Console.WriteLine($"mysqlConnectionUri: {mysqlConnectionUri}");
        var optionsBuilder = new DbContextOptionsBuilder<ImdbContext>()
            .UseMySql(
                mysqlConnectionUri,
                ServerVersion.AutoDetect(mysqlConnectionUri),
                b => b.MigrationsAssembly("SeedData")
            )
            .EnableDetailedErrors();
        //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        return new ImdbContext(optionsBuilder.Options);
    }

    /// <summary>
    /// This connection is used to get data from MySql without applying migrations.
    /// This will prevent to re-apply migrations when reading data.
    /// </summary>
    /// <param name="connectionString"> MySql connection string to get data from</param>
    /// <returns>ImdbContext </returns>
    public static ImdbContext MySqlConnectionToGetData(string connectionString = "MySqlConnectionString")
    {
        var mysqlConnectionUri = Environment.GetEnvironmentVariable(connectionString)!;
        var optionsBuilder = new DbContextOptionsBuilder<ImdbContext>()
            .UseMySql(
                mysqlConnectionUri,
                ServerVersion.AutoDetect(mysqlConnectionUri)
            );
        //    .EnableDetailedErrors()
        //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        return new ImdbContext(optionsBuilder.Options);
    }
}
