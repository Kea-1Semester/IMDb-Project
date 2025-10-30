using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class Episodes
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } 

    [BsonElement("titleIdParent")]
    [BsonRepresentation(BsonType.String)]
    public Guid TitleIdParent { get; set; }
    [BsonElement("titleIdChild")]
    [BsonRepresentation(BsonType.String)]
    public Guid TitleIdChild { get; set; }

    [BsonElement("seasonNumber")]
    public int SeasonNumber { get; set; }
    [BsonElement("episodeNumber")]
    public int EpisodeNumber { get; set; }

}