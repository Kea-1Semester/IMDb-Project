using EfCoreModelsLib.Models.MongoDb.SupportClasses;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb;

public class TitleMongoDb
{
    // Use string for the MongoDB Id to avoid Guid/ObjectId serialization issues

    [BsonId]
    [BsonElement("id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } = new ObjectId();

    [BsonElement("titleId")]
    [BsonRepresentation(BsonType.String)]
    public Guid TitleId { get; set; } = Guid.Empty;

    [BsonElement("titleType")]
    public string TitleType { get; set; } = string.Empty;

    [BsonElement("primaryTitle")]
    public string PrimaryTitle { get; set; } = string.Empty;
    [BsonElement("originalTitle")]
    public string OriginalTitle { get; set; } = string.Empty;

    [BsonElement("isAdult")]
    public bool IsAdult { get; set; }

    [BsonElement("startYear")]
    public int StartYear { get; set; }

    [BsonElement("endYear")]
    public int? EndYear { get; set; }

    [BsonElement("runtimeMinutes")]
    public int? RuntimeMinutes { get; set; }

    [BsonElement("genres")]
    public List<string> Genres { get; set; } = new();

    [BsonElement("actors")]
    public List<CastMember> Actors { get; set; } = new();

    [BsonElement("directors")]
    public List<PersonRef> Directors { get; set; } = new();

    [BsonElement("writers")]
    public List<PersonRef> Writers { get; set; } = new();

    [BsonElement("ratings")]
    public Rating? Ratings { get; set; }

    [BsonElement("aliases")]
    public List<Alias> Aliases { get; set; } = new();

    [BsonElement("comments")]
    public List<Comment> Comments { get; set; } = new();
    [BsonElement("episodes")]
    public List<Episodes> Episodes { get; set; } = new();
}
