using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class CastMember : PersonRef
{
    [BsonElement("role")]
    public string Role { get; set; } = string.Empty;
}