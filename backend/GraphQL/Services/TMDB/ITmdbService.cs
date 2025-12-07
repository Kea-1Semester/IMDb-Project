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
    public string Title { get; set; }
    [JsonPropertyName("original_title")]
    public string Original_title { get; set; }
    [JsonPropertyName("overview")]
    public string Overview { get; set; }
    [JsonPropertyName("poster_path")]
    public string Poster_Path { get; set; }
    [JsonPropertyName("release_date")]
    public string Release_Date { get; set; }
    [JsonPropertyName("vote_average")]
    public double Vote_Average { get; set; }

}

public record MovieResponse
{
    [JsonPropertyName("results")]
    public required List<MovieDetails> Results { get; set; }
    [JsonPropertyName("page")]
    public int Page { get; set; }

}
