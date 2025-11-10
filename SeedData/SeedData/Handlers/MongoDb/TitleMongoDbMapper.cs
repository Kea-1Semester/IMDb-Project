using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.Handler;
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
            Id = ObjectId.GenerateNewId(),
            TitleId = titles.TitleId,
            TitleType = titles.TitleType,
            PrimaryTitle = titles.PrimaryTitle,
            OriginalTitle = titles.OriginalTitle,
            IsAdult = titles.IsAdult,
            StartYear = titles.StartYear,
            EndYear = titles.EndYear,
            RuntimeMinutes = titles.RuntimeMinutes,
            Genres = titles.GenresGenre.Select(g => g.Genre)
                .Where(g => !string.IsNullOrWhiteSpace(g) && g != "\\N")
                .ToList(),
            Actors = titles.Actors?
                         .Where(a => a is { PersonsPerson: not null } &&
                                     !string.IsNullOrWhiteSpace(a.PersonsPerson.Name) &&
                                     a.PersonsPersonId != Guid.Empty)
                         .Select(a => new CastMember
                         {
                             Id = a.PersonsPersonId,
                             Name = a.PersonsPerson.Name,
                             Role = string.IsNullOrWhiteSpace(a.Role) || a.Role == "\\N" ? null : a.Role
                         })
                         .ToList()
                     ?? [],
            Directors = titles.Directors?
                .Where(d => d.PersonsPerson != null &&
                            !string.IsNullOrEmpty(d.PersonsPerson.Name)
                )
                .Select(d => new PersonRef
                {
                    Id = d.PersonsPersonId,
                    Name = d.PersonsPerson.Name
                }).ToList() ?? [],
            Writers = titles.Writers?
                .Where(w => w.PersonsPerson != null && !string.IsNullOrEmpty(w.PersonsPerson.Name))
                .Select(w => new PersonRef
                {
                    Id = w.PersonsPersonId,
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
                    Attributes = a.AttributesAttribute != null
                        ? a.AttributesAttribute.Select(attr => attr.Attribute).ToList()
                        : new List<string>(),
                    Types = a.TypesType != null ? a.TypesType.Select(type => type.Type).ToList() : new List<string>()
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
                    TitleIdParent = e.TitleIdParent,
                    TitleIdChild = e.TitleIdChild,
                    SeasonNumber = e.SeasonNumber,
                    EpisodeNumber = e.EpisodeNumber
                }).ToList() ?? []
        };
    }

    private static async Task<List<TitleMongoDb>> ListTitleMongoData(ImdbContext imdbContext, int pageSize, int page)
    {
        List<Titles> alTitlesList = new();
        for (var i = 0; i < page; i++)
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
                .AsSplitQuery()
                .OrderBy(t => t.TitleId)
                .ThenBy(t => t.StartYear)
                .Skip(i * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (titlesFromMysql.Count == 0)
                break;

            alTitlesList.AddRange(titlesFromMysql);
            Console.WriteLine($"Fetched {alTitlesList.Count} titles from MySQL.. page {i + 1}/{page}");
            await Task.Delay(1000);


        }
        return alTitlesList.Select(MapTitleMongoDb).DistinctBy(t => t.TitleId).ToList();
    }

    /// <summary>
    /// By default, connects to the MySql cloud instance to get data.
    /// To Change connection, provide the connection string parameter.
    /// <example>
    /// Call this method as follows:
    /// <code>
    /// await TitleMongoDbMapper.MigrateToMongoDb(
    ///     pageSize: 5000,
    ///     page: 10,
    ///     connectionString: "YourMySqlConnectionStringHere"
    /// );
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="connectionString">MySql connection string to get data from</param>
    /// <param name="pageSize"></param>
    /// <param name="page"></param>
    public static async Task MigrateToMongoDb(int pageSize = 1000, int page = 0, string? connectionString = null)
    {
        var mongoDbData = new List<TitleMongoDb>();
        var mongoDbPersonData = new List<PersonsMongoDb>();


        // 1. Read from MySQL and join data that match the MongoDB Schema
        await using var mysqlContext = MySqlSettings.MySqlConnectionToGetData();

        await using var contextMongo = MongoDbSettings.MongoDbConnection();


        await contextMongo.Database.EnsureDeletedAsync();

        mongoDbData.AddRange(await ListTitleMongoData(mysqlContext, pageSize: pageSize, page: page));
        mongoDbPersonData.AddRange(await ListPersonsMongoData(mysqlContext, pageSize: pageSize, page: page));

        // 2. Validate / Create MongoDB and Collections with indexes

        var compoundIndex = new List<BsonDocument>
        {
            new BsonDocument
            {
                { "primaryTitle", 1 },
                { "originalTitle", 1 },
                // on ratings
                { "ratings.averageRating", -1 },
                { "ratings.numVotes", -1 }
            }
        };
        var singleFieldIndex = new BsonDocument
        {
            { "titleId", 1 }
        };

        await MongoSchemaInitializer<TitleMongoDb>.EnsureCollectionSchema(
            connectionString: Environment.GetEnvironmentVariable("MongoDbConnectionString")!,
            databaseName: Environment.GetEnvironmentVariable("MongoDbDatabase")!,
            nameof(SchemaName.Titles),
            TitlesValidator.GetSchema(),
            compoundIndex,
            singleFieldIndex
        );

        var singleFieldIndexPerson = new BsonDocument
        {
            { "name", 1 }
        };

        //Validate Persons Collection Schema
        await MongoSchemaInitializer<PersonsMongoDb>.EnsureCollectionSchema(
           Environment.GetEnvironmentVariable("MongoDbConnectionString")!,
           databaseName: Environment.GetEnvironmentVariable("MongoDbDatabase")!,
           nameof(SchemaName.Persons),
            PersonValidator.GetSchema(),
           singleFieldIndex: singleFieldIndexPerson
           );


        Console.WriteLine("Ensuring the MongoDB database exists...");
        try
        {
            await contextMongo.Database.EnsureCreatedAsync();
            Console.WriteLine("MongoDB database ensured.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
        }

        //// 3. Migrate Data to MongoDB
        //await contextMongo.Titles.AddRangeAsync(mongoDbData);
        ////await contextMongo.Titles.AddRangeAsync
        //await contextMongo.SaveChangesAsync();
        const int batchSize = 10000;
        for (var i = 0; i < mongoDbData.Count; i++)
        {
            var batch = mongoDbData
                .Skip(i * batchSize)
                .Take(batchSize).ToList();
            if (batch.Count == 0)
                break;
            await contextMongo.Titles.AddRangeAsync(batch);
            await contextMongo.SaveChangesAsync();
            await Task.Delay(2000);

            Console.WriteLine(
                $"Inserted {Math.Min((i + 1) * batchSize, mongoDbData.Count)} of {mongoDbData.Count} titles into MongoDB...");
        }

        for (var i = 0; i < mongoDbPersonData.Count; i++)
        {
            var batch = mongoDbPersonData
                .Skip(i * batchSize)
                .Take(batchSize).ToList();
            if (batch.Count == 0)
                break;
            await contextMongo.Persons.AddRangeAsync(batch);
            await contextMongo.SaveChangesAsync();
            await Task.Delay(2000);

            Console.WriteLine(
                $"Inserted {Math.Min((i + 1) * batchSize, mongoDbPersonData.Count)} of {mongoDbPersonData.Count} persons into MongoDB...");
        }



    }

    private static PersonsMongoDb MapPersonsMongoDb(Persons persons)
    {
        return new PersonsMongoDb
        {
            Id = ObjectId.GenerateNewId(),
            Guid = persons.PersonId,
            Name = persons.Name,
            BirthYear = persons.BirthYear,
            EndYear = persons.EndYear,
            Professions = persons.Professions?
                .Where(p => p != null && !string.IsNullOrWhiteSpace(p.Profession))
                .Select(p => p.Profession)
                .ToList() ?? [],
            KnownFor = persons.KnownFor?
                .Where(k => k is { TitlesTitle: not null })
                .Select(k => new KnownForTitle
                {
                    TitleId = k.TitlesTitleId,
                    TitleName = k.TitlesTitle!.PrimaryTitle
                })
                .ToList() ?? []
        };
    }

    private static async Task<List<PersonsMongoDb>> ListPersonsMongoData(ImdbContext imdbContext, int pageSize, int page)
    {
        List<Persons> allPersonsList = [];
        for (var i = 0; i < page; i++)
        {
            var personsFromMysql = await imdbContext.Persons
                .Include(p => p.Professions)
                .Include(p => p.KnownFor)
                .ThenInclude(k => k.TitlesTitle)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(p => p.PersonId)
                .ThenBy(p => p.BirthYear)
                .Skip(i * pageSize)
                .Take(pageSize)
                .ToListAsync();


            if (personsFromMysql.Count == 0)
                break;

            allPersonsList.AddRange(personsFromMysql);
            Console.WriteLine($"Fetched {allPersonsList.Count} persons from MySQL.. page {i + 1}/{page}");
            await Task.Delay(1000);
        }
        return allPersonsList.Select(MapPersonsMongoDb).DistinctBy(p => p.Guid).ToList();
    }
}

public enum SchemaName
{
    Titles,
    Persons
}