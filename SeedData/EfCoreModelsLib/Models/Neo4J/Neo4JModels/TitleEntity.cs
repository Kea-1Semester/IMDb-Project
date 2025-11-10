using EfCoreModelsLib.Models.Neo4J.Neo4JModels;

namespace EfCoreModelsLib.models.Neo4J.Neo4JModels
{
    public partial class TitlesEntity
    {
        public Guid TitleId { get; set; }
        public string TitleType { get; set; } = "";
        public string PrimaryTitle { get; set; } = "";
        public string OriginalTitle { get; set; } = "";
        public bool IsAdult { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int RuntimeMinutes { get; set; }

        // Title-To-Title Relationships
        public List<TitlesEntity> EpisodesInSerie { get; set; } = new();
        public TitlesEntity Series { get; set; }

        // Persons Relationships
        public List<PersonsEntity> DirectedBy { get; set; } = new();
        public List<PersonsEntity> WrittenBy { get; set; } = new();
        public List<PersonsEntity> PlayedAsBy { get; set; } = new();

        // Genres Relationships
        public List<GenresEntity> HasGenres { get; set; } = new();


        // Ratings, Aliases, Comments Relationships
        public RatingsEntity HasRating { get; set; }
        public List<AliasesEntity> HasAliases { get; set; } = new();
        public List<CommentsEntity> HasComments { get; set; } = new();
    }
}