using DotNetEnv;
using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.SchemaValidator;
using EfCoreModelsLib.Models.Mysql;
using EfCoreModelsLib.Models.Neo4J.Neo4JModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SeedData.DbConnection;
using SeedData.Handlers;
using SeedData.Handlers.MongoDb;
using SeedData.Handlers.Neo4j.Mappers;

namespace SeedData;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Env.TraversePath().Load();
        await SeedData();
        await TitleMongoDbMapper.MigrateToMongoDb(40000, 4);
        await MigrateToNeo4J();
    }

    private static async Task SeedData()
    {
        var projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;


        // MySqlConnection default connection is connected to the cloud sql instance
        await using (var context = MySqlSettings.MySqlConnection("ConnectionStringAiven"))
        {
            Console.WriteLine("Ensuring the database exists...");

            try
            {
                await context.Database.EnsureDeletedAsync();

                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}: {ex.InnerException?.Message}");
            }

            Console.WriteLine("Ran database migrations.");


            if (Environment.GetEnvironmentVariable("SEED_SAMPLE_DATA") == "true")
            {
                var dataRoot = Environment.GetEnvironmentVariable("DATA_ROOT")!;
                var sqlFile = Path.Combine(dataRoot, "mysqldump.sql");
                Console.WriteLine($"Data Root: {dataRoot}");


                await context.Database.ExecuteSqlRawAsync(await File.ReadAllTextAsync(sqlFile));


                // open file advance folder and execute all sql files except Drop.sql
                foreach (var file in Directory.GetFiles(Path.Combine(dataRoot, "advance"), "*.sql"))
                {
                    if (Path.GetFileName(file) == "Drop.sql")
                        continue;
                    Console.WriteLine($"Executing Advanced File: {file}");
                    await context.Database.ExecuteSqlRawAsync(await File.ReadAllTextAsync(file));

                }

                Console.WriteLine("Seeded Sample Data into MySQL Database.");
            }
            else
            {
                var dataFolder = Path.Combine(projectRoot!, "data");
                var mysqlFolder = Path.Combine(projectRoot!, "mysql");

                var titleBasicPath = Path.Combine(dataFolder, "title.basics.tsv");
                var titleRatingsPath = Path.Combine(dataFolder, "title.ratings.tsv");
                var nameBasicPath = Path.Combine(dataFolder, "name.basics.tsv");
                var titleCrewPath = Path.Combine(dataFolder, "title.crew.tsv");
                var titleEpisodePath = Path.Combine(dataFolder, "title.episode.tsv");
                var titlePrincipalsPath = Path.Combine(dataFolder, "title.principals.tsv");
                var titleAkasPath = Path.Combine(dataFolder, "title.akas.tsv");

                var titleIdsDict = await TitleBasicsHandler.SeedTitleBasics(context, titleBasicPath, 100000);
                var personIdsDict = await AddPersonToDb.AddPerson(context, nameBasicPath, 100000, titleIdsDict);
                await AddCrewToDb.AddCrew(context, titleCrewPath, 50000, titleIdsDict, personIdsDict);
                await AddEpisode.AddEpisodes(context, titleEpisodePath, titleIdsDict);
                await AddActor.AddActorToDb(context, titlePrincipalsPath, titleIdsDict, personIdsDict);
                await AddRating.AddRatingToDb(context, titleRatingsPath, titleIdsDict);
                await AddAkas.AddAkasToDb(context, titleAkasPath, 50000, titleIdsDict);

                Console.WriteLine("Seeded Sample Data into MySQL Database.");

                foreach (var file in Directory.GetFiles(Path.Combine(mysqlFolder, "advance"), "*.sql"))
                {
                    if (Path.GetFileName(file) == "Drop.sql")
                        continue;

                    Console.WriteLine($"Executing Advanced File: {file}");

                    await context.Database.ExecuteSqlRawAsync(await File.ReadAllTextAsync(file));

                }





            }
        }

        Console.WriteLine("Program Completed");
    }


    private static async Task MigrateToNeo4J()
    {
        Env.TraversePath().Load();

        // 1) Schema (Community-safe UNIQUE constraints for alle labels du bruger)
        await EfCoreModelsLib.Models.Neo4J.Handler.Neo4jSchemaInitializer.EnsureConstraintsAsync(
            Environment.GetEnvironmentVariable("NEO4J_URI")!,
            Environment.GetEnvironmentVariable("NEO4J_USER")!,
            Environment.GetEnvironmentVariable("NEO4J_PASSWORD")!);

        // 2) Eksempeldata (erstat evt. med dine data fra MySQL)

        // Attributes og Types relateret data
        var attrColor = new AttributesEntity { AttributeId = Guid.NewGuid(), Attribute = "Color" };
        var typeMovie = new TypesEntity { TypeId = Guid.NewGuid(), Type = "movie" };

        //Titles relateret data
        var genre = new GenresEntity { GenreId = Guid.NewGuid(), Genre = "Comedy" };
        var rating = new RatingsEntity { RatingId = Guid.NewGuid(), AverageRating = 7.5, NumVotes = 1500 };
        var Comment = new CommentsEntity { CommentId = Guid.NewGuid(), Comment = "This is a comment" };

        //Persons relateret data
        var profession_actor = new ProfessionsEntity { ProfessionId = Guid.NewGuid(), Profession = "actor" };
        var profession_writer = new ProfessionsEntity { ProfessionId = Guid.NewGuid(), Profession = "writer" };
        var profession_director = new ProfessionsEntity { ProfessionId = Guid.NewGuid(), Profession = "director" };

        // Persons
        var person_1 = new PersonsEntity
        {
            PersonId = Guid.NewGuid(),
            Name = "John Doe",
            BirthYear = 1980,
            EndYear = null,
            HasProfessions = new() { profession_actor },
        };

        var person_2 = new PersonsEntity
        {
            PersonId = Guid.NewGuid(),
            Name = "Jane Smith",
            BirthYear = 1988,
            EndYear = null,
            HasProfessions = new() { profession_director, profession_writer },
        };

        var alias = new AliasesEntity
        {
            AliasId = Guid.NewGuid(),
            Region = "DK",
            Language = "da",
            IsOriginalTitle = false,
            Title = "Den danske titel",
            HasAttributes = new() { attrColor },
            HasTypes = new() { typeMovie }
        };

        var titles = new TitlesEntity
        {
            TitleId = Guid.NewGuid(),
            TitleType = "movie",
            PrimaryTitle = "The Primary Title",
            OriginalTitle = "The Original Title",
            IsAdult = false,
            StartYear = 2020,
            EndYear = 2021,
            RuntimeMinutes = 120,
            HasAliases = new() { alias },
            HasGenres = new() { genre },
            HasComments = new() { Comment },
            HasRating = rating,
        };

        var titles_Serie = new TitlesEntity
        {
            TitleId = Guid.NewGuid(),
            TitleType = "tvSeries",
            PrimaryTitle = "Another Movie",
            OriginalTitle = "Another Original Title",
            IsAdult = false,
            StartYear = 2021,
            EndYear = 2022,
            RuntimeMinutes = null,
            HasGenres = new() { genre },
            HasRating = rating,
        };

        var titles_Episode = new TitlesEntity
        {
            TitleId = Guid.NewGuid(),
            TitleType = "tvEpisode",
            PrimaryTitle = "Another Movie",
            OriginalTitle = "Another Original Title",
            IsAdult = false,
            StartYear = 2021,
            EndYear = 2022,
            RuntimeMinutes = 90,
            HasGenres = new() { genre },
            HasRating = rating,
        };
        
        titles_Episode.Series = titles_Serie;
        titles_Serie.Episodes.Add(titles_Episode);

        person_1.KnownFor.Add(titles);

        titles.Writer.Add(person_2);
        titles.Director.Add(person_2);
        titles.Actor.Add(new ActedInRelationship
        {
            Person = person_1,
            Title = titles,
            Role = "Aquaman"
        });

        // 3) Kør ALT i én call
        var payload = new Neo4jMapper.UpsertPayload
        {
            Attributes = new[] { attrColor },
            Types = new[] { typeMovie },
            Aliases = new[] { alias },
            Genres = new[] { genre },
            Titles = new[] { titles, titles_Serie, titles_Episode },
            Ratings = new[] { rating },
            Comments = new[] { Comment },
            Professions = new[] { profession_actor, profession_writer, profession_director },
            Persons = new[] { person_1, person_2}
        };

        await Neo4jMapper.UpsertAll(payload, batchSize: 1000);

        Console.WriteLine("✅ Neo4j upsert complete.");
    
    }
}


