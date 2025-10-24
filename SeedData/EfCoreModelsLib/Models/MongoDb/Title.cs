using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EfCoreModelsLib.Models.MongoDb;

public class Title
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonId]
    [BsonElement("titleId")]
    public Guid TitleId { get; set; }

    [BsonElement("titleType")]
    public string TitleType { get; set; } = null!;

    [BsonElement("primaryTitle")]
    public string PrimaryTitle { get; set; } = null!;

    [BsonElement("originalTitle")]
    public string OriginalTitle { get; set; } = null!;

    [BsonElement("isAdult")]
    public bool IsAdult { get; set; }

    [BsonElement("startYear")]
    public int StartYear { get; set; }

    [BsonElement("endYear")]
    public int? EndYear { get; set; }

    [BsonElement("runtimeMinutes")]
    public int? RuntimeMinutes { get; set; }

    [BsonElement("genres")]
    public List<string> Genres { get; set; } = new();

    [BsonElement("actors")]
    public List<CastMember> Actors { get; set; } = new();

    [BsonElement("directors")]
    public List<PersonRef> Directors { get; set; } = new();

    [BsonElement("writers")]
    public List<PersonRef> Writers { get; set; } = new();

    [BsonElement("ratings")]
    public Rating? Ratings { get; set; }

    [BsonElement("aliases")]
    public List<Alias> Aliases { get; set; } = new();

    [BsonElement("comments")]
    public List<Comment> Comments { get; set; } = new();
    
}


// Supporting classes

public class PersonRef
{
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid PersonId { get; set; } 

    public string Name { get; set; } = null!;
}

public class CastMember : PersonRef
{
    public string Role { get; set; } = null!;
}

public class Rating
{
    public double AverageRating { get; set; }
    public int NumVotes { get; set; }
}

public class Alias
{
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid AliasId { get; set; }
    public string Region { get; set; } = null!;
    public string Language { get; set; } = null!;
    public bool IsOriginalTitle { get; set; }
    public string Title { get; set; } = null!;
}

public class Comment
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string CommentId { get; set; } = null!;
    public string CommentText { get; set; } = null!;
}