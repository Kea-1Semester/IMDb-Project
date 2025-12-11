using DotNetEnv;
using GraphQL.Services.TMDB;

namespace integration.tests.TMDB
{
    [Explicit("Runs only manually or in CI with proper environment variables set")]
    public class ImdbServiceTests
    {
        private HttpClient _httpClient;
        private ITmdbService _service;

        [SetUp]
        public void SetUp()
        {
            Env.TraversePath().Load();
            var apiKey = Environment.GetEnvironmentVariable(
                "TMDB_API_KEY"
            )!;
            var baseUrl = Environment.GetEnvironmentVariable(
                "TMDB_BASE_URL"
            )!;

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("TMDB_API_KEY environment variable is not set.");
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            try
            {
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                _service = new TmdbService(_httpClient);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialize TmdbService", ex);
            }

        }

        [Test]
        public void GetTmdb()
        {
            var result = _service.GetAll().Result;
            Assert.That(result != null);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }



    }
}
