using System.Text.Json.Serialization;

namespace GraphQL.Services.TMDB
{
    public interface ITmdbService
    {
        Task<List<MovieDetails>?> GetAll(int? newPage = null);
    }
}


public record MovieDetails 
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("original_title")]
    public required string OriginalTitle { get; set; }
    [JsonPropertyName("overview")]
    public required string Overview { get; set; }
    [JsonPropertyName("poster_path")]
    public required string PosterPath { get; set; }
    [JsonPropertyName("release_date")]
    public required string ReleaseDate { get; set; }
    [JsonPropertyName("vote_average")]
    public required double VoteAverage { get; set; }

}

public record MovieResponse
{
    [JsonPropertyName("results")]
    public required List<MovieDetails> Results { get; set; }
    [JsonPropertyName("page")]
    public int Page { get; set; }

}
