using MongoDB.Bson;
using MongoDB.Driver;

namespace EfCoreModelsLib.Models.MongoDb
{
    public static class MongoSchemaInitializer
    {
        public static void EnsureCollectionSchema(
            string connectionString,
            string databaseName,
            string collectionName,
            BsonDocument schema
        )
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            var collectionList = database.ListCollectionNames().ToList();
            if (!collectionList.Contains(collectionName))
            {
                var options = new CreateCollectionOptions<BsonDocument>()
                {
                    Validator = new BsonDocumentFilterDefinition<BsonDocument>(schema)
                };
                database.CreateCollection(collectionName, options);
            }
            else
            {
                var command = new BsonDocument
                {
                    { "collMod", collectionName },
                    { "validator", schema }
                };
                database.RunCommand<BsonDocument>(command);

            }
        }
    }
}

