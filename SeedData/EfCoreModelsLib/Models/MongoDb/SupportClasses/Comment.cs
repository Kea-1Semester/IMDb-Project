using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class Comment
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string CommentId { get; set; } = null!;
    public string CommentText { get; set; } = null!;
}