using EfCoreModelsLib.Models.Mysql;
using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator;

public abstract class DirectorsBsonDocument
{
    public static BsonDocument GetSchema()
    {
        const string bsonType = "bsonType";

        return new BsonDocument
        {
            { bsonType, "array" },
            {
                "items", new BsonDocument
                {
                    { bsonType, "object" },
                    { "required", new BsonArray {  "id", "name" } },
                    { "additionalProperties", false },
                    {
                        "properties", new BsonDocument
                        {
                            { "id", new BsonDocument(bsonType, new BsonArray { "binData", "string" }) },
                            { "name", new BsonDocument(bsonType, "string") }
                        }
                    }
                }
            }

        };
    }

}