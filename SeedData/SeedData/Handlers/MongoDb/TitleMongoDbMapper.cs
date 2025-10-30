using EfCoreModelsLib.Models.MongoDb;
using EfCoreModelsLib.Models.MongoDb.SupportClasses;
using EfCoreModelsLib.Models.Mysql;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace SeedData.Handlers.MongoDb;

public static class TitleMongoDbMapper
{
    public static TitleMongoDb MapTitleMongoDb(Titles titles)
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


    public static async Task<List<TitleMongoDb>> ListTitleMongoData(ImdbContext imdbContext)
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
}