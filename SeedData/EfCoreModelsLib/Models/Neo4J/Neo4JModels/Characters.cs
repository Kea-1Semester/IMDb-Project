namespace EfCoreModelsLib.models.Neo4J.Neo4JModels
{
    public partial class Characters
    {
        // Actors is Renamed to Characters to be more inclusive
        public Guid CharacterId { get; set; }
        public string CharacterName { get; set; }

        // Persons Relationships
        public List<Persons> PlayedBy { get; set; } = new();
        public List<Titles> PlayedIn { get; set; } = new();
    }
}