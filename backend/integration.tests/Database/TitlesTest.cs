using DotNetEnv;
using EfCoreModelsLib.DTO;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Repos.Mysql;
using GraphQL.Services.Mysql;
using integration.tests.DbConnection;
using integration.tests.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace integration.tests.Database
{
    public class TitlesTest
    {
        private MysqlTitlesRepo _titlesRepo;
        private IMysqlTitlesService _titlesService;
        private IDbContextFactory<ImdbContext> _dbContextFactory;
        private TitlesDto _titlesDto = null!;


        private ImdbContext DbContext => _dbContextFactory.CreateDbContext();

        [SetUp]
        public void Setup()
        {
            Env.TraversePath().Load();
            var connectionString = Environment.GetEnvironmentVariable(
                "MySqlConnectionString_test"
            )!;

            var services = new ServiceCollection();
            services.AddDbContextFactory<ImdbContext>(options =>
            {
                options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString));
            });
            var serviceProvider = services.BuildServiceProvider();
            _dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<ImdbContext>>();

            // Create database and apply migrations
            using var context = DbContext;
            context.Database.EnsureCreated();

            context.Titles.AddRange(MockTitles.GetMockTitles());
            context.SaveChanges();
            _titlesRepo = new MysqlTitlesRepo(new TestDbContextFactory(_dbContextFactory));
            _titlesService = new MysqlTitlesService(_titlesRepo);


        }
        [Test]
        public void TestGetTitles()
        {
            // act
            var titles = _titlesService.GetMysqlTitles().ToList();

            // assert
            Assert.That(titles.Count, Is.EqualTo(2));
        }
        [Test]
        public void TestGetTitleById()
        {
            // arrange
            var titleId = MockTitles.GetMockTitles()[0].TitleId;
            // act
            var titles = _titlesService.GetMysqlTitles().FirstOrDefault(t => t.TitleId == titleId);
            // assert
            Assert.That(titleId, Is.EqualTo(titles!.TitleId));
        }

        [Test]
        public void TestAddTitles()
        {
            // arrange
            _titlesDto = new TitlesDto
            {
                TitleType = "movie",
                PrimaryTitle = "The Matrix",
                OriginalTitle = "The Matrix",
                IsAdult = false,
                StartYear = 1999,
                EndYear = null
            };

            // act
            var createdTitle = _titlesService.CreateMysqlTitle(_titlesDto);

            // assert
            Assert.That(
                new
                {
                    createdTitle.Result.TitleType,
                    createdTitle.Result.PrimaryTitle,
                    createdTitle.Result.OriginalTitle,
                    createdTitle.Result.IsAdult,
                    createdTitle.Result.StartYear,
                    createdTitle.Result.EndYear
                },
                Is.EqualTo(
                    new
                    {
                        _titlesDto.TitleType,
                        _titlesDto.PrimaryTitle,
                        _titlesDto.OriginalTitle,
                        _titlesDto.IsAdult,
                        _titlesDto.StartYear,
                        _titlesDto.EndYear
                    }
                )
            );
        }

        [TearDown]
        public async ValueTask DisposeAsync()
        {
            await DbContext.Database.EnsureDeletedAsync();
            await _titlesRepo.DisposeAsync();
        }
    }
}
