using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator
{
    public class TitlesValidator
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
                                { "titleId", "titleType", "primaryTitle", "originalTitle", "isAdult", "startYear" }
                        },
                        {
                            "properties", new BsonDocument
                            {
                                { "titleId", new BsonDocument("bsonType", "binData") },
                                { "titleType", new BsonDocument("bsonType", "string") },
                                { "primaryTitle", new BsonDocument("bsonType", "string") },
                                { "originalTitle", new BsonDocument("bsonType", "string") },
                                { "isAdult", new BsonDocument("bsonType", "bool") },
                                { "startYear", new BsonDocument("bsonType", "int") },
                                { "endYear", new BsonDocument("bsonType", new BsonArray { "int", "null" }) },
                                { "runtimeMinutes", new BsonDocument("bsonType", new BsonArray { "int", "null" }) },
                                {
                                    "genres", new BsonDocument
                                    {
                                        { "bsonType", "array" },
                                        { "items", new BsonDocument("bsonType", "string") }
                                    }
                                },
                                {
                                    "actors", new BsonDocument
                                    {
                                        { "bsonType", "array" },
                                        {
                                            "items", new BsonDocument
                                              {
                                                { "bsonType", "object" },
                                                { "required", new BsonArray { "personId", "name" } },
                                                {
                                                    "properties", new BsonDocument
                                                    {
                                                        { "personId", new BsonDocument("bsonType", "binData") },
                                                        { "name", new BsonDocument("bsonType", "string") },
                                                        { "role", new BsonDocument("bsonType", "string") }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                {
                                    "directors", new BsonDocument
                                    {
                                        { "bsonType", "array" },
                                        {
                                            "items", new BsonDocument
                                            {
                                                { "bsonType", "object" },
                                                { "required", new BsonArray { "personId", "name" } },
                                                {
                                                    "properties", new BsonDocument
                                                    {
                                                        { "personId", new BsonDocument("bsonType", "binData") },
                                                        { "name", new BsonDocument("bsonType", "string") }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                {
                                    "writers", new BsonDocument
                                    {
                                        { "bsonType", "array" },
                                        {
                                            "items", new BsonDocument
                                            {
                                                { "bsonType", "object" },
                                                { "required", new BsonArray { "personId", "name" } },
                                                {
                                                    "properties", new BsonDocument
                                                    {
                                                        { "personId", new BsonDocument("bsonType", "binData") },
                                                        { "name", new BsonDocument("bsonType", "string") }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                {
                                    "ratings", new BsonDocument
                                    {
                                        { "bsonType", new BsonArray { "object", "null" } },
                                        {
                                            "properties", new BsonDocument
                                            {
                                                { "averageRating", new BsonDocument("bsonType", "double") },
                                                { "numVotes", new BsonDocument("bsonType", "int") }
                                            }
                                        }
                                    }
                                },
                                {
                                    "aliases", new BsonDocument
                                    {
                                        { "bsonType", "array" },
                                        {
                                            "items", new BsonDocument
                                            {
                                                { "bsonType", "object" },
                                                { "required", new BsonArray { "region", "title" } },
                                                {
                                                    "properties", new BsonDocument
                                                    {
                                                        { "region", new BsonDocument("bsonType", "string") },
                                                        { "title", new BsonDocument("bsonType", "string") }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                {
                                    "comments", new BsonDocument
                                    {
                                        { "bsonType", "array" },
                                        {
                                            "items", new BsonDocument
                                            {
                                                { "bsonType", "object" },
                                                { "required", new BsonArray { "userId", "text" } },
                                                {
                                                    "properties", new BsonDocument
                                                    {
                                                        { "userId", new BsonDocument("bsonType", "binData") },
                                                        { "text", new BsonDocument("bsonType", "string") },
                                                        { "createdAt", new BsonDocument("bsonType", "date") }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

       
    }
}