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
                    //{ "required", new BsonArray { "personId", "name" } },
                    {
                        "properties", new BsonDocument
                        {
                            { "personId", new BsonDocument("bsonType", new BsonArray { "binData", "string" }) },
                            { "name", new BsonDocument("bsonType", "string") }
                        }
                    }
                }
            }


        };
    }
}