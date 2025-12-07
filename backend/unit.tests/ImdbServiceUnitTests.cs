using GraphQL.Services.TMDB;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace unit.tests
{
    public class ImdbServiceUnitTests
    {
        private const string UriString = "https://api.themoviedb.org/3";

        private ITmdbService? _tmdbService;
        private HttpClient _httpClient;

        private readonly MovieResponse _fakeResponse = new()
        {
            Page = 1,
            Results = [new MovieDetails { Id = 10, Title = "Test Movie" }]
        };

        [Test]
        public async Task GetAll_Page1_ReturnsMovies()
        {
            // arrange
            var json = JsonSerializer.Serialize(_fakeResponse);

            var handlerMock = CreateMockHttp(json);
            _httpClient = new HttpClient(handlerMock.Object);
            _httpClient.BaseAddress = new Uri(UriString);
            _tmdbService = new TmdbService(_httpClient);

            // act
            var result = await _tmdbService.GetAll();

            // assert
            Assert.That(result?[0], Is.EqualTo(_fakeResponse.Results[0]));
            Assert.That(result.Count, Is.EqualTo(1));

        }
        [Test]
        public Task GetAll_ApiError_ThrowsException()
        {
            // arrange
            var handlerMock = CreateMockHttp("", statusCode: HttpStatusCode.InternalServerError);
            _httpClient = new HttpClient(handlerMock.Object);
            _httpClient.BaseAddress = new Uri(UriString);
            _tmdbService = new TmdbService(_httpClient);
            // act & assert
            Assert.ThrowsAsync<Exception>(async () => await _tmdbService.GetAll());
            return Task.CompletedTask;
        }
        [Test]
        public async Task GetAll_EmptyList_ReturnsEmpty()
        {
            //arrange
            var jsonEmpty = "{ \"page\": 1, \"results\": [] }";

            var handlerMock = CreateMockHttp(jsonEmpty);
            _httpClient = new HttpClient(handlerMock.Object);
            _httpClient.BaseAddress = new Uri(UriString);
            _tmdbService = new TmdbService(_httpClient);
            // act
            var result = await _tmdbService.GetAll();

            // assert
            Assert.That(result.Count, Is.EqualTo(0));

        }

        [Test]
        public void GetAll_ModelChanged_Throws()
        {
            // arrange
            // TMDb changed "results" to "items"
            var changedJson = "{ \"page\": 1, \"items\": [] }";

            var handlerMock = CreateMockHttp(changedJson);
            _httpClient = new HttpClient(handlerMock.Object);
            _httpClient.BaseAddress = new Uri(UriString);

            _tmdbService = new TmdbService(_httpClient);

            // act & assert
            Assert.ThrowsAsync<JsonException>(async () => await _tmdbService.GetAll());

        }


        [TearDown]
        public void Teardown()
        {
            _httpClient.Dispose();

        }

        private Mock<HttpMessageHandler> CreateMockHttp(string json, string method = "SendAsync", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    method,
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(json)
                });
            return handlerMock;
        }
    }
}
