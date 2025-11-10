using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace EfCoreModelsLib.Models.MongoDb.SchemaValidator
{
    public abstract class PersonValidator
    {
        public static BsonDocument GetSchema()
        {
            return new BsonDocument
            {
                {
                    "$jsonSchema", new BsonDocument
                    {
                        { "bsonType", "object" },
                        { "required", new BsonArray { "personId", "name", "birthYear", "professions", "knownForTitles" } },
                        { "additionalProperties", false },
                        {
                            "properties", new BsonDocument
                            {
                                {"_id", new BsonDocument("bsonType", "objectId") },
                                { "personId", new BsonDocument("bsonType", new BsonArray { "binData", "string" }) }, // allow Guid or string
                                { "name", new BsonDocument("bsonType", "string") },
                                { "birthYear", new BsonDocument("bsonType", "int") },
                                { "endYear", new BsonDocument("bsonType", new BsonArray { "int", "null" }) },
                                {
                                    "professions", new BsonDocument
                                    {
                                        { "bsonType", "array" },
                                        {
                                            "items", new BsonDocument
                                            {
                                                { "bsonType", "string" }
                                            }
                                        }
                                    }
                                },
                                {
                                    "knownForTitles", new BsonDocument
                                    {
                                        { "bsonType", "array" },
                                        {
                                            "items", new BsonDocument
                                            {
                                                { "bsonType", "object" },
                                                { "additionalProperties", false },
                                                {
                                                    "required", new BsonArray { "titleId", "titleName" }
                                                },
                                                {
                                                    "properties", new BsonDocument
                                                    {
                                                        { "titleId", new BsonDocument("bsonType",  new BsonArray { "binData", "string" }) }, // Guid as string
                                                        { "titleName", new BsonDocument("bsonType", "string") }
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
