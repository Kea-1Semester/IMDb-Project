using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator
{
    public abstract class TitlesValidator
    {
        public static BsonDocument GetSchema()
        {
            return new BsonDocument
            {
                {
                    "$jsonSchema", new BsonDocument
                    {
                        { "bsonType", "object" },
                        {
                            "required", new BsonArray
                                { "id","titleType", "primaryTitle", "originalTitle", "isAdult", "startYear" }
                        },
                        {
                            "properties", new BsonDocument
                            {
                                { "id", new BsonDocument("bsonType", new BsonArray { "binData", "string" }) }, // allow Guid or string
                                { "titleType", new BsonDocument("bsonType", "string") },
                                { "primaryTitle", new BsonDocument("bsonType", "string") },
                                { "originalTitle", new BsonDocument("bsonType", "string") },
                                { "isAdult", new BsonDocument("bsonType", "bool") },
                                { "startYear", new BsonDocument("bsonType", "int") },
                                { "endYear", new BsonDocument("bsonType", new BsonArray { "int", "null" }) },
                                { "runtimeMinutes", new BsonDocument("bsonType", new BsonArray { "int", "null" }) },
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