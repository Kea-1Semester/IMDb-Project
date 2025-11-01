using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator;

public abstract class AliasesBsonDocument
{
    public static BsonDocument GetSchema()
    {
        return new BsonDocument
        {
            { "bsonType", "array" },
            {
                "items", new BsonDocument
                {
                    { "bsonType", "object" },
                    { "required", new BsonArray { "id", "title" } },
                    { "additionalProperties", false },
                    {
                        "properties", new BsonDocument
                        {
                            { "id", new BsonDocument("bsonType", new BsonArray { "binData", "string" }) },
                            { "region", new BsonDocument("bsonType", new BsonArray { "string", "null" }) },
                            { "title", new BsonDocument("bsonType", "string") },
                            { "language", new BsonDocument("bsonType", new BsonArray { "string", "null" }) },
                            { "isOriginalTitle", new BsonDocument("bsonType", new BsonArray { "bool", "null" }) },
                            {
                                "types", new BsonDocument
                                {
                                    { "bsonType", "array" },
                                    { "items", new BsonDocument("bsonType", new BsonArray { "string", "null" }) }
                                }
                            },
                            {
                                "attributes", new BsonDocument
                                {
                                    { "bsonType", "array" },
                                    { "items", new BsonDocument("bsonType", new BsonArray { "string", "null" }) }
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}