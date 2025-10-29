using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class Alias
{
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid AliasId { get; set; }
    public string Region { get; set; } = null!;
    public string Language { get; set; } = null!;
    public bool IsOriginalTitle { get; set; }
    public string Title { get; set; } = null!;
}