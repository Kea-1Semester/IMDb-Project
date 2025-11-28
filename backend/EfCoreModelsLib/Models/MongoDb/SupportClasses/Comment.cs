using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class Comment
{
    [BsonElement("id")]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } 
    [BsonElement("commentText")]
    public string CommentText { get; set; } = string.Empty;

    // Consider adding userId and CreateAt
    // public Guid UserId { get; set; }
    // public DateTime CreatedAt { get; set; }
}