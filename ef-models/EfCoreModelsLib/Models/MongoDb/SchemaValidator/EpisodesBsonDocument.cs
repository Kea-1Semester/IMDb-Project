using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator;

public abstract class EpisodesBsonDocument
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
                    { "required", new BsonArray { "titleIdParent", "titleIdChild", "seasonNumber", "episodeNumber" } },
                    { "additionalProperties", false },
                    {
                        "properties", new BsonDocument
                        {
                            { "titleIdParent", new BsonDocument(bsonType, new BsonArray { "binData", "string" }) },
                            { "titleIdChild", new BsonDocument(bsonType, new BsonArray { "binData", "string" }) },
                            { "seasonNumber", new BsonDocument(bsonType, "int") },
                            { "episodeNumber", new BsonDocument(bsonType, "int") }
                        }
                    }
                }
            }


        };
    }

}