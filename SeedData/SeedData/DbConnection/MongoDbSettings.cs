using EfCoreModelsLib.Models.MongoDb.ObjDbContext;
using Microsoft.EntityFrameworkCore;

namespace SeedData.DbConnection;

public static class MongoDbSettings
{
    public static ImdbContextMongoDb MongoDbConnection(
        string connectionStr = "MongoDbConnectionString",
        string databaseNameKey = "MongoDbDatabase")
    {
        var mongoDbConnectionUri = Environment.GetEnvironmentVariable(connectionStr)!;
        var dbName = Environment.GetEnvironmentVariable(databaseNameKey)!;

        Console.WriteLine($"mongoDbConnectionUri: {mongoDbConnectionUri}");
        Console.WriteLine($"dbName: {dbName}");


        var optionsBuilder = new DbContextOptionsBuilder<ImdbContextMongoDb>()
            .UseMongoDB(mongoDbConnectionUri, dbName)
            .EnableDetailedErrors();
        return new ImdbContextMongoDb(optionsBuilder.Options);
    }
}