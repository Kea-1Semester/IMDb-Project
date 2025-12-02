using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator;

public abstract class AliasesBsonDocument
{
    public static BsonDocument GetSchema()
    {
        const string bsonType = "bsonType";
        const string stringType = "string";

        return new BsonDocument
        {
            { bsonType, "array" },
            {
                "items", new BsonDocument
                {
                    { bsonType, "object" },
                    { "required", new BsonArray { "id", "title" } },
                    { "additionalProperties", false },
                    {
                        "properties", new BsonDocument
                        {
                            { "id", new BsonDocument(bsonType, new BsonArray { "binData", stringType }) },
                            { "region", new BsonDocument(bsonType, new BsonArray { stringType, "null" }) },
                            { "title", new BsonDocument(bsonType, stringType) },
                            { "language", new BsonDocument(bsonType, new BsonArray { stringType, "null" }) },
                            { "isOriginalTitle", new BsonDocument(bsonType, new BsonArray { "bool", "null" }) },
                            {
                                "types", new BsonDocument
                                {
                                    { bsonType, "array" },
                                    { "items", new BsonDocument(bsonType, new BsonArray { stringType, "null" }) }
                                }
                            },
                            {
                                "attributes", new BsonDocument
                                {
                                    { bsonType, "array" },
                                    { "items", new BsonDocument(bsonType, new BsonArray { stringType, "null" }) }
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}