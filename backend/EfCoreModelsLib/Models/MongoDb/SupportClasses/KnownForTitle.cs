using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class KnownForTitle
{
    [BsonElement("titleId")]
    [BsonRepresentation(BsonType.String)]
    public Guid TitleId { get; set; }
    [BsonElement("titleName")]
    public string TitleName { get; set; } = null!;
}