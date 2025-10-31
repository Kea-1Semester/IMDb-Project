using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class KnownForTitle
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid TitleId { get; set; }
    public string TitleName { get; set; }
}