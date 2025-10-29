using EfCoreModelsLib.Models.MongoDb;

namespace SeedData.Handlers.MongoDb;

public static class TitleMongoDbHandler
{
    public static TitleMongoDb ConvertToTitleMongoDb(object titleMysql)
    {
        var titleMongo = new TitleMongoDb()
        {
            //Map properties from titleMysql to titleMongo
            //TitleId = titleMysql.GetType().GetProperty("TitleId")!.GetValue(titleMysql) is Guid titleId ? titleId : Guid.Empty,




        };

        return titleMongo;
    }

}