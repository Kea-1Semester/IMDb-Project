namespace EfCoreModelsLib.Models.Neo4J.Neo4JModels
{
    public partial class CharactersEntity
    {
        // Actors is Renamed to Characters to be more inclusive
        public Guid CharacterId { get; set; }
        public string CharacterName { get; set; } = "";

        // Persons Relationships
        public List<PersonsEntity> PlayedBy { get; set; } = new();
        public List<TitlesEntity> PlayedIn { get; set; } = new();
    }
}