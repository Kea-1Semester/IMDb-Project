using DotNetEnv;
using EfCoreModelsLib.Models.MongoDb.ObjDbContext;
using Microsoft.EntityFrameworkCore;

namespace SeedData.DbConnection;

public static class MongoDbSettings
{
    public static ImdbContextMongoDb MongoDbConnection(string connectionStr = "MongoDbConnectionStr")
    {
        var mongoDbConnectionUri = Env.GetString(connectionStr);
        var optionsBuilder = new DbContextOptionsBuilder<ImdbContextMongoDb>()
            .UseMongoDB(mongoDbConnectionUri)
            .EnableDetailedErrors();
        return new ImdbContextMongoDb(optionsBuilder.Options);
    }
}