namespace EfCoreModelsLib.Models.Neo4J.Neo4JModels
{
    public partial class AliasesEntity
    {
        public Guid AliasId { get; set; }
        public string Region { get; set; } = "";
        public string Language { get; set; } = "";
        public bool IsOriginalTitle { get; set; }
        public string Title { get; set; } = "";

        // Relationships
        public List<AttributesEntity> HasAttributes { get; set; } = new();
        public List<TypesEntity> HasTypes { get; set; } = new();
    }
}