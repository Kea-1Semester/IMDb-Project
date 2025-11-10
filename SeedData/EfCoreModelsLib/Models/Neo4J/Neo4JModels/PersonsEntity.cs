using EfCoreModelsLib.Models.Neo4J.Neo4JModels;

namespace EfCoreModelsLib.models.Neo4J.Neo4JModels
{
    public class PersonsEntity
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; } = "";
        public int BirthYear { get; set; }
        public int EndYear { get; set; }

        // Professions Relationships
        public List<ProfessionsEntity> HasProfessions { get; set; } = new();

        // Characters Relationships
        public List<CharactersEntity> PlayedAs { get; set; } = new();

        // Titles Relationships
        public List<TitlesEntity> KnownFor { get; set; } = new();
        public List<TitlesEntity> Directed { get; set; } = new();
        public List<TitlesEntity> Wrote { get; set; } = new();
        public List<TitlesEntity> ActedInAs { get; set; } = new();
    }
}