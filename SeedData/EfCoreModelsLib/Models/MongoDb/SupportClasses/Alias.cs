using EfCoreModelsLib.Models.Mysql;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class Alias
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public string Region { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public bool IsOriginalTitle { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> Types { get; set; } = [];
    public List<string> Attributes { get; set; } = [];

}