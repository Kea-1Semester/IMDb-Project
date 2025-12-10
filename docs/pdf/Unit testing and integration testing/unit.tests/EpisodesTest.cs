using System.ComponentModel.DataAnnotations;
using EfCoreModelsLib.DTO;

namespace unit.tests;

public class EpisodesTest
{
    private EpisodesDto _episode;

    [SetUp]
    public void Setup()
    {
        _episode = new EpisodesDto()
        {
            ParentId = Guid.NewGuid(),
            ChildId = Guid.NewGuid(),
            EpisodeNumber = 1,
            SeasonNumber = 1
        };
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(500)]
    [TestCase(998)]
    [TestCase(999)]
    public void ValidEpisodeNumberTest(int episodeNumber)
    {
        _episode.EpisodeNumber = episodeNumber;
        Assert.That(_episode.Validate, Throws.Nothing);
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(55)]
    [TestCase(95)]
    [TestCase(99)]
    public void ValidSeasonNumberTest(int seasonNumber)
    {
        _episode.SeasonNumber = seasonNumber;
        Assert.That(_episode.Validate, Throws.Nothing);
    }

    [TestCase(-1)]
    [TestCase(-5)]
    [TestCase(1000)]
    [TestCase(2000)]
    [TestCase(int.MaxValue)]
    public void InValidEpisodeNumberTest(int episodeNumber)
    {
        _episode.EpisodeNumber = episodeNumber;
        Assert.That(_episode.Validate, Throws.TypeOf<ValidationException>());
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-50)]
    [TestCase(100)]
    [TestCase(501)]
    public void InValidSeasonNumberTest(int seasonNumber)
    {
        _episode.SeasonNumber = seasonNumber;
        Assert.That(_episode.Validate, Throws.TypeOf<ValidationException>());
    }
}