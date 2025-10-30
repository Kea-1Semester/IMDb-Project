using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class PersonRef
{
    [BsonId]  
    [BsonRepresentation(BsonType.String)]
    public Guid PersonId { get; set; }

    public string Name { get; set; } = string.Empty;
}