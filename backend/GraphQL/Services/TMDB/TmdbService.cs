namespace GraphQL.Services.TMDB
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;

        public TmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<MovieDetails>?> GetAll(int? newPage = null)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<MovieResponse>(
                    newPage.HasValue ? $"discover/movie?page={newPage}" : "discover/movie"
                );
                return result?.Results;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to fetch data from TMDB API", ex);
            }
        }


    }
}
