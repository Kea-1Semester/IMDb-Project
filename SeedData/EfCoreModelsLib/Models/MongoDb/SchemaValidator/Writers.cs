using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator;

public abstract class Writers
{
    public static BsonDocument GetSchema()
    {
        return new BsonDocument
        {
            { "bsonType", new BsonArray { "array", "null" } },
            {
                "items", new BsonDocument
                {
                    { "bsonType", "object" },
                    { "required", new BsonArray { "id", "name" } },
                    {
                        "properties", new BsonDocument
                        {
                            { "id", new BsonDocument("bsonType", new BsonArray { "binData", "string" }) },
                            { "name", new BsonDocument("bsonType", "string") }
                        }
                    }
                }
            }


        };
    }
}