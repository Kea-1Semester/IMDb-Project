using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator
{
    public abstract class TitlesValidator
    {
        public static BsonDocument GetSchema()
        {
            const string bsonType = "bsonType";
            const string stringType = "string";

            return new BsonDocument
            {
                {
                    "$jsonSchema", new BsonDocument
                    {
                        { bsonType, "object" },
                        { "required", new BsonArray { "titleId", "titleType", "primaryTitle", "originalTitle", "isAdult", "startYear" } },
                        { "additionalProperties", false },
                        {
                            "properties", new BsonDocument
                            {
                                {"_id", new BsonDocument(bsonType, "objectId") },
                                { "titleId", new BsonDocument(bsonType, new BsonArray { "binData", stringType }) }, // allow Guid or string
                                { "titleType", new BsonDocument(bsonType, stringType) },
                                { "primaryTitle", new BsonDocument(bsonType, stringType) },
                                { "originalTitle", new BsonDocument(bsonType, stringType) },
                                { "isAdult", new BsonDocument(bsonType, "bool") },
                                { "startYear", new BsonDocument(bsonType, "int") },
                                { "endYear", new BsonDocument(bsonType, new BsonArray { "int", "null" }) },
                                { "runtimeMinutes", new BsonDocument(bsonType, new BsonArray { "int", "null" }) },
                                {
                                    "genres", GenresBsonDocument.GetSchema()
                                },
                                {
                                    "actors",ActorsBsonDocument.GetSchema()
                                },
                                {
                                    "directors", DirectorsBsonDocument.GetSchema()
                                },
                                {
                                    "writers", Writers.GetSchema()
                                },
                                {
                                    "ratings", RatingsBsonDocument.GetSchema()
                                },
                                {
                                    "aliases", AliasesBsonDocument.GetSchema()
                                },
                                {
                                    "comments", CommentsDocument.GetSchema()
                                },
                                {
                                    "episodes", EpisodesBsonDocument.GetSchema()
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}