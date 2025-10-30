using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator
{
    public abstract class GenresBsonDocument
    {
        public static BsonDocument GetSchema()
        {
            return new BsonDocument
            {
                { "bsonType", "array" },
                { "items", new BsonDocument("bsonType", "string") }

            };
        }
    }
}