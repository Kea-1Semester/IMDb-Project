using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class PersonRef
{
    [BsonElement("id")]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
}