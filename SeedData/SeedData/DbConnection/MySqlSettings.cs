using DotNetEnv;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;

namespace SeedData.DbConnection;

public static class MySqlSettings
{
    public static ImdbContext MySqlConnection(string connectionStr = "ConnectionString")
    {
        var mysqlConnectionUri = Env.GetString(connectionStr);
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
    public static ImdbContext MySqlConnectionToGetData(string connectionStr = "ConnectionString")
    {
        var mysqlConnectionUri = Env.GetString(connectionStr);
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
