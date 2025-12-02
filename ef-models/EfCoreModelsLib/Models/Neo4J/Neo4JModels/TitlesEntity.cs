namespace EfCoreModelsLib.Models.Neo4J.Neo4JModels
{
    public class TitlesEntity
    {
        public Guid TitleId { get; set; }
        public string TitleType { get; set; } = "";
        public string PrimaryTitle { get; set; } = "";
        public string OriginalTitle { get; set; } = "";
        public bool IsAdult { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }

        // Title-To-Title Relationships
        public List<TitlesEntity> Episodes { get; set; } = new();
        public TitlesEntity? Series { get; set; }

        // Persons Relationships
        public List<PersonsEntity> Director { get; set; } = new();
        public List<PersonsEntity> Writer { get; set; } = new();
        public List<ActedInRelationship> Actor { get; set; } = new();

        // Genres Relationships
        public List<GenresEntity> HasGenres { get; set; } = new();

        // Ratings, Aliases, Comments Relationships
        public RatingsEntity? HasRating { get; set; }
        public List<AliasesEntity> HasAliases { get; set; } = new();
        public List<CommentsEntity> HasComments { get; set; } = new();
    }
}