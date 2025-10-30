using DotNetEnv;
using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.SchemaValidator;
using EfCoreModelsLib.Models.MongoDb.SupportClasses;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using SeedData.DbConnection;

namespace SeedData.Handlers.MongoDb;

public static class TitleMongoDbMapper
{
    private static TitleMongoDb MapTitleMongoDb(Titles titles)
    {
        return new TitleMongoDb
        {
            Id = titles.TitleId,
            TitleType = titles.TitleType,
            PrimaryTitle = titles.PrimaryTitle,
            OriginalTitle = titles.OriginalTitle,
            IsAdult = titles.IsAdult,
            StartYear = titles.StartYear,
            EndYear = titles.EndYear,
            RuntimeMinutes = titles.RuntimeMinutes,
            Genres = titles.GenresGenre.Select(g => g.Genre).ToList(),
            Actors = titles.Actors?
                    .Where(a => a is { PersonsPerson: not null } &&
                                !string.IsNullOrWhiteSpace(a.PersonsPerson.Name) &&
                                a.PersonsPersonId != Guid.Empty)
                         .Select(a => new CastMember
                         {
                             PersonId = a.PersonsPersonId,
                             Name = a.PersonsPerson.Name,
                             Role = a.Role
                         })
                         .ToList()
                     ?? [],
            Directors = titles.Directors?
                .Where(d => d.PersonsPerson != null &&
                            !string.IsNullOrEmpty(d.PersonsPerson.Name)
                )
                .Select(d => new PersonRef
                {
                    PersonId = d.PersonsPersonId,
                    Name = d.PersonsPerson.Name
                }).ToList() ?? [],
            Writers = titles.Writers?
                .Where(w => w.PersonsPerson != null && !string.IsNullOrEmpty(w.PersonsPerson.Name))
                .Select(w => new PersonRef
                {
                    PersonId = w.PersonsPersonId,
                    Name = w.PersonsPerson.Name
                }).ToList() ?? [],
            Ratings = titles.Ratings != null && titles.Ratings.Any()
                ? new Rating
                {
                    AverageRating = titles.Ratings.First().AverageRating,
                    NumVotes = titles.Ratings.First().NumVotes
                }
                : null,
            Aliases = titles.Aliases?
                .Where(a => a != null)
                .Select(a => new Alias
                {
                    Id = a.AliasId,
                    Region = a.Region,
                    Language = a.Language,
                    IsOriginalTitle = a.IsOriginalTitle,
                    Title = a.Title,
                    Attributes = a.AttributesAttribute != null ? a.AttributesAttribute.Select(attr => attr.Attribute).ToList() : new List<string>(),
                    Types = a.TypesType != null ? a.TypesType.Select(type => type.Type).ToList() : new List<string>(),
                }).ToList() ?? [],
            Comments = titles.Comments?
                .Where(c => c != null)
                .Select(c => new Comment
                {
                    Id = c.CommentId,
                    CommentText = c.Comment
                }).ToList() ?? [],
            Episodes = titles.EpisodesTitleIdParentNavigation?
                .Where(e => e != null)
                .Select(e => new EfCoreModelsLib.Models.MongoDb.SupportClasses.Episodes
                {
                    Id = ObjectId.GenerateNewId(),
                    TitleIdParent = e.TitleIdParent,
                    TitleIdChild = e.TitleIdChild,
                    SeasonNumber = e.SeasonNumber,
                    EpisodeNumber = e.EpisodeNumber

                }).ToList() ?? []

        };

    }

    private static async Task<List<TitleMongoDb>> ListTitleMongoData(ImdbContext imdbContext)
    {
        var titlesFromMysql = await imdbContext.Titles
            .Include(t => t.GenresGenre)
            .Include(t => t.Aliases)
            .Include(t => t.Comments)
            .Include(t => t.Ratings)
            .Include(t => t.EpisodesTitleIdParentNavigation)
            .ThenInclude(e => e.TitleIdChildNavigation)
            .Include(t => t.Actors)
            .ThenInclude(a => a.PersonsPerson)
            .Include(t => t.Directors)
            .ThenInclude(d => d.PersonsPerson)
            .Include(r => r.Writers)
            .ThenInclude(w => w.PersonsPerson)
            .AsNoTracking()
            .Take(10000)
            .ToListAsync();


        var titlesList = titlesFromMysql
            .Where(t =>
                t.Aliases.Any() &&
                t.Ratings != null &&
                t.Actors.Any() &&
                t.Directors.Any() &&
                t.Writers.Any()
            )
            .Take(100)
            .ToList();

        var listWithEpisodes = titlesFromMysql
            .Where(t =>
                (
                    t.EpisodesTitleIdParentNavigation.Count != 0 ||
                    t.EpisodesTitleIdChildNavigation.Count != 0
                )
            )
            .ToList();

        // combine both lists
        return titlesList.Union(listWithEpisodes).Distinct()
            .Select(MapTitleMongoDb)
            .Distinct()
            .ToList();

    }

    /// <summary>
    /// By default, connects to the MySql cloud instance to get data.
    /// To Change connection, provide the connection string parameter. 
    /// </summary>
    /// <param name="connectionString">MySql connection string to get data from</param>
    public static async Task MigrateToMongoDb(string connectionString)
    {
        var mongoDbData = new List<TitleMongoDb>();

        // 1. Read from MySQL and join data that match the MongoDB Schema
        await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData(connectionString);

        mongoDbData.AddRange(await ListTitleMongoData(mysqlContext));

        // 2. Validate / Create MongoDB and Collections
        // Validate Titles Collection Schema
        MongoSchemaInitializer.EnsureCollectionSchema(
            Env.GetString("MongoDbConnectionStr"),
            "imdb-mongo-db",
            nameof(SchemaName.Titles),
            TitlesValidator.GetSchema());

        //Validate Persons Collection Schema
        //MongoSchemaInitializer.EnsureCollectionSchema(
        //    Env.GetString("MongoDbConnectionStr"),
        //    "imdb-mongo-db",
        //    "Persons",
        //    PersonsValidator.GetSchema());


        // 3. Migrate Data to MongoDB
        await using var contextMongo = MongoDbSettings.MongoDbConnection();

        Console.WriteLine("Ensuring the MongoDB database exists...");
        try
        {
            await contextMongo.Database.EnsureDeletedAsync();
            await contextMongo.Database.EnsureCreatedAsync();
            Console.WriteLine("MongoDB database ensured.");


        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
        }

        await contextMongo.Titles.AddRangeAsync(mongoDbData);
        await contextMongo.SaveChangesAsync();
    }
}

public enum SchemaName
{
    Titles,
    Persons

}