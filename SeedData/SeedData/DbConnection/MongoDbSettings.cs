using DotNetEnv;
using EfCoreModelsLib.Models.MongoDb.ObjDbContext;
using Microsoft.EntityFrameworkCore;

namespace SeedData.DbConnection;

public static class MongoDbSettings
{
    public static ImdbContextMongoDb MongoDbConnection(
        string connectionStr = "MongoDbConnectionStr",
        string databaseNameKey = "MongoDbDatabase")
    {
        var mongoDbConnectionUri = Env.GetString(connectionStr);
        var dbName = Env.GetString(databaseNameKey);

        var optionsBuilder = new DbContextOptionsBuilder<ImdbContextMongoDb>()
            .UseMongoDB(mongoDbConnectionUri, dbName)
            .EnableDetailedErrors();
        return new ImdbContextMongoDb(optionsBuilder.Options);
    }
}