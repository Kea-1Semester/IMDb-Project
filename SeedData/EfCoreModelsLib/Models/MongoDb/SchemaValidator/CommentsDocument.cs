using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator;

public abstract class CommentsDocument
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
                    { "required", new BsonArray { "id" } },
                    {
                        "properties", new BsonDocument
                        {
                            { "id", new BsonDocument("bsonType", new BsonArray { "binData", "string" }) },
                            { "userId", new BsonDocument("bsonType", new BsonArray { "binData", "string", "null" }) },
                            { "commentText", new BsonDocument("bsonType", new BsonArray { "string", "null" }) }
                        }
                    }
                }
            }


        };
    }

}