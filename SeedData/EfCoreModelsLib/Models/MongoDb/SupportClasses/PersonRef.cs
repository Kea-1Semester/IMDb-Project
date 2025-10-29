using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class PersonRef
{
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid PersonId { get; set; } 

    public string Name { get; set; } = null!;
}