using EfCoreModelsLib.Models.Neo4J.Neo4JModels;

namespace EfCoreModelsLib.models.Neo4J.Neo4JModels
{
    public class Persons
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public int EndYear { get; set; }

        // Professions Relationships
        public List<Professions> HasProfessions { get; set; } = new();

        // Characters Relationships
        public List<Characters> PlayedAs { get; set; } = new();

        // Titles Relationships
        public List<Titles> KnownFor { get; set; } = new();
        public List<Titles> Directed { get; set; } = new();
        public List<Titles> Wrote { get; set; } = new();
        public List<Titles> ActedInAs { get; set; } = new();
    }
}