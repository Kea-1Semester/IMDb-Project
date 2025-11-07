namespace EfCoreModelsLib.Models.Neo4J.Neo4JModels
{
    public partial class Aliases
    {
        public Guid AliasId { get; set; }
        public string Region { get; set; }
        public string Language { get; set; }
        public bool IsOriginalTitle { get; set; }
        public string Title { get; set; }

        // Relationships
        public List<Attributes> HasAttributes { get; set; } = new();
        public List<Types> HasTypes { get; set; } = new();
    }
}