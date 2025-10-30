using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } 
    public string CommentText { get; set; } = string.Empty;

    // Consider adding userId and CreateAt
    // public Guid UserId { get; set; }
    // public DateTime CreatedAt { get; set; }
}