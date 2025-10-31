using MongoDB.Bson.Serialization.Attributes;
namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class Rating
{
    [BsonElement("AverageRating")]
    public double AverageRating { get; set; }
    [BsonElement("NumVotes")]
    public int NumVotes { get; set; }
}