using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator;

public abstract class RatingsBsonDocument
{
    public static BsonDocument GetSchema()
    {
        return new BsonDocument
        {
            { "bsonType", new BsonArray { "object", "null" } },
            {
                "properties", new BsonDocument
                {
                    { "averageRating", new BsonDocument("bsonType", "double") },
                    { "numVotes", new BsonDocument("bsonType", "int") }
                }
            }

        };

    }

}