using EfCoreModelsLib.Models.MongoDb.SupportClasses;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb
{
    public class PersonsMongoDb
    {
        [BsonId]
        public Guid Guid { get; set; }
        [BsonElement("name")]
        public string Name { get; set; } = null!;
        [BsonElement("birthYear")]
        public int BirthYear { get; set; }
        [BsonElement("endYear")]
        public int? EndYear { get; set; }
        [BsonElement("professions")]
        public List<string> Professions { get; set; } = null!;

    }
}
