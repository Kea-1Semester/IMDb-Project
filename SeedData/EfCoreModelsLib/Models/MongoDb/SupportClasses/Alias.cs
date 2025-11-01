using EfCoreModelsLib.Models.Mysql;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class Alias
{
    [BsonElement("id")]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("region")]
    public string Region { get; set; } = string.Empty;
    [BsonElement("language")]
    public string Language { get; set; } = string.Empty;
    [BsonElement("isOriginalTitle")]
    public bool IsOriginalTitle { get; set; }
    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;
    [BsonElement("types")]
    public List<string> Types { get; set; } = new();
    [BsonElement("attributes")]
    public List<string> Attributes { get; set; } = new();

}