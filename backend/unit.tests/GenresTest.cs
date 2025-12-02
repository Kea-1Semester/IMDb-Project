using System.ComponentModel.DataAnnotations;
using EfCoreModelsLib.DTO;

namespace unit.tests;

public class GenreTest
{
    [TestCase("Bio")]
    [TestCase("Adventures")]
    [TestCase("AdeventureAdeventureAdeventureAdeventureAdeventure")]
    public void ValidGenresTest(string genreName)
    {
        var genre = new GenresDto()
        {
            Genre = genreName
        };
        Assert.That(genre.Validate, Throws.Nothing);
    }

    [TestCase("B")]
    [TestCase("Bi")]
    [TestCase("AdventuresAdeventuresAdeventuresAdeventuresAdeventuress")]
    [TestCase("AdventuresAdeventuresAdeventuresAdeventuresAdeventuresAdeventures")]
    public void InValidGenresTest(string genreName)
    {
        var genre = new GenresDto()
        {
            Genre = genreName
        };
        Assert.That(genre.Validate, Throws.TypeOf<ValidationException>());
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("#5-char")]
    public void EgdeCases(string genreName)
    {
        var genre = new GenresDto()
        {
            Genre = genreName
        };
        Assert.That(genre.Validate, Throws.TypeOf<ValidationException>());
    }
}