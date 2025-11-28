namespace EfCoreModelsLib.Models.Neo4J.Neo4JModels
{
    public partial class ActedInRelationship
    {
        // Actors is Renamed to Characters to be more inclusive

        // Persons Relationships
        public PersonsEntity Person { get; set; } = null!;
        public TitlesEntity Title { get; set; } = null!;
        public string Role { get; set; } = "";
    }
}