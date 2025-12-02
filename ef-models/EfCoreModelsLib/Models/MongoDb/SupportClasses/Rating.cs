    using MongoDB.Bson.Serialization.Attributes;
namespace EfCoreModelsLib.Models.MongoDb.SupportClasses;

public class Rating
{
    [BsonElement("averageRating")]
    public double AverageRating { get; set; }
    [BsonElement("numVotes")]
    public int NumVotes { get; set; }
}