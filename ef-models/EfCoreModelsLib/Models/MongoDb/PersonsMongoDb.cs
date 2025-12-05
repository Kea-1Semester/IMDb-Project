using EfCoreModelsLib.Models.MongoDb.SupportClasses;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb
{
    public class PersonsMongoDb
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("personId")]
        [BsonRepresentation(BsonType.String)]
        public Guid Guid { get; init; }
        [BsonElement("name")]
        public string Name { get; init; } = null!;
        [BsonElement("birthYear")]
        public int BirthYear { get; init; }
        [BsonElement("endYear")]
        public int? EndYear { get; init; }
        [BsonElement("professions")]
        public List<string> Professions { get; init; } = null!;
        [BsonElement("knownForTitles")]
        public List<KnownForTitle> KnownFor { get; init; } = null!;

    }
}
