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
        private const string BsonTypeKey = "bsonType";
        private const string String = "string";

        public static BsonDocument GetSchema()
        {
            return new BsonDocument
            {
                {
                    "$jsonSchema", new BsonDocument
                    {
                        { BsonTypeKey, "object" },
                        {
                            "required",
                            new BsonArray { "personId", "name", "birthYear", "professions", "knownForTitles" }
                        },
                        { "additionalProperties", false },
                        {
                            "properties", new BsonDocument
                            {
                                { "_id", new BsonDocument(BsonTypeKey, "objectId") },
                                {
                                    "personId", new BsonDocument(BsonTypeKey, new BsonArray { "binData", String })
                                }, // allow Guid or string
                                { "name", new BsonDocument(BsonTypeKey, String) },
                                { "birthYear", new BsonDocument(BsonTypeKey, "int") },
                                { "endYear", new BsonDocument(BsonTypeKey, new BsonArray { "int", "null" }) },
                                {
                                    "professions", new BsonDocument
                                    {
                                        { BsonTypeKey, "array" },
                                        {
                                            "items", new BsonDocument
                                            {
                                                { BsonTypeKey, String }
                                            }
                                        }
                                    }
                                },
                                {
                                    "knownForTitles", new BsonDocument
                                    {
                                        { BsonTypeKey, "array" },
                                        {
                                            "items", new BsonDocument
                                            {
                                                { BsonTypeKey, "object" },
                                                { "additionalProperties", false },
                                                {
                                                    "required", new BsonArray { "titleId", "titleName" }
                                                },
                                                {
                                                    "properties", new BsonDocument
                                                    {
                                                        {
                                                            "titleId",
                                                            new BsonDocument(BsonTypeKey,
                                                                new BsonArray { "binData", String })
                                                        }, // Guid as string
                                                        { "titleName", new BsonDocument(BsonTypeKey, String) }
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
