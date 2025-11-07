namespace EfCoreModelsLib.Models.Neo4J.Neo4JModels
{
    public class Ratings
    {
        public Guid RatingId { get; set; }
        public double AverageRating { get; set; }
        public int NumVotes { get; set; }
    }
}