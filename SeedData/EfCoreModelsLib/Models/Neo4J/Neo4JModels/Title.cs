using EfCoreModelsLib.Models.Neo4J.Neo4JModels;

namespace EfCoreModelsLib.models.Neo4J.Neo4JModels
{
    public partial class Titles
    {
        public Guid TitleId { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int RuntimeMinutes { get; set; }

        // Title-To-Title Relationships
        public List<Titles> EpisodesInSerie { get; set; } = new();
        public Titles Series { get; set; }

        // Persons Relationships
        public List<Persons> DirectedBy { get; set; } = new();
        public List<Persons> WrittenBy { get; set; } = new();
        public List<Persons> PlayedAsBy { get; set; } = new();

        // Genres Relationships
        public List<Genres> HasGenres { get; set; } = new();


        // Ratings, Aliases, Comments Relationships
        public Ratings HasRating { get; set; }
        public List<Aliases> HasAliases { get; set; } = new();
        public List<Comments> HasComments { get; set; } = new();
    }
}