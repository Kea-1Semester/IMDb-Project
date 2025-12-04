using System.ComponentModel.DataAnnotations;
using EfCoreModelsLib.DTO;

namespace unit.tests;

public class AliasesTest
{
    private AliasesDto _alias;

    [SetUp]
    public void Setup()
    {
        _alias = new AliasesDto()
        {
            TitleId = Guid.NewGuid(),
            Region = "EU",
            Language = "EN",
            IsOriginalTitle = false,
            Title = "Sample Title"
        };
    }

    [TestCase("US")]
    [TestCase("USA")]
    [TestCase("Asia")]
    [TestCase("Asian")]
    public void ValidRegionTest(string region)
    {
        _alias.Region = region;
        Assert.That(_alias.Validate, Throws.Nothing);
    }

    [TestCase("DA")]
    [TestCase("ENG")]
    [TestCase("EN-US")]
    public void ValidLanguageTest(string language)
    {
        _alias.Language = language;
        Assert.That(_alias.Validate, Throws.Nothing);
    }

    [TestCase("Froze")]
    [TestCase("The Mysterious Journey Across the Endless Sea Saga")]
    [TestCase("The Godfather, Casablanca, Psycho, It’s a Wonderful Life, Raging Bull, Once Upon a Time in the West, Goodfellas, Pulp Fiction, Apocalypse Now, Dr. Strangelove or: How I Learned to Stop Worrying and Love the Bomb")]
    [TestCase("The Godfather, Casablanca, Psycho, It’s a Wonderful Life, Raging Bull, Once Upon a Time in the West, Goodfellas, Pulp Fiction, Apocalypse Now, Dr. Strangelove or: How I Learned to Stop Worrying and Love the Bombs")]
    [TestCase("star-wars")]
    public void ValidTitleTest(string title)
    {
        _alias.Title = title;
        Assert.That(_alias.Validate, Throws.Nothing);
    }

    [TestCase("U")]
    [TestCase("UNITED")]
    [TestCase("OsfQnIiZzia4DtYhx3MPaBf5YTynhSg5GNBjqiIrORZJoj3fDKuik4xnKK1HTuVxNiTC49MAMhsxsYjEHrH9Vvg71xKF9Oh7SyAzAQGsYcz7aMXNhgNy5491AXw7bdHmhHkhUtVyXTPd7crlqfgbpF")]
    public void InValidRegionTest(string region)
    {
        _alias.Region = region;
        Assert.That(_alias.Validate, Throws.TypeOf<ValidationException>());
    }

    [TestCase("D")]
    [TestCase("ENGLISHA")]
    [TestCase("24bOLRxaSc")]
    public void InValidLanguageTest(string language)
    {
        _alias.Language = language;
        Assert.That(_alias.Validate, Throws.TypeOf<ValidationException>());
    }

    [TestCase("Fro")]
    [TestCase("Froz")]
    [TestCase("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")]
    [TestCase("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")]
    [TestCase("star#wars")]
    public void InValidTitleTest(string title)
    {
        _alias.Title = title;
        Assert.That(_alias.Validate, Throws.TypeOf<ValidationException>());
    }

    [TestCase("%&#¤")]
    public void EgdeCaseRegionTest(string region)
    {
        _alias.Region = region;
        Assert.That(_alias.Validate, Throws.TypeOf<ValidationException>());
    }

    [TestCase(",")]
    [TestCase(" ")]
    public void EgdeCaseLanguageTest(string language)
    {
        _alias.Language = language;
        Assert.That(_alias.Validate, Throws.TypeOf<ValidationException>());
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("#asbas-char")]
    public void EgdeCaseTitleTest(string title)
    {
        _alias.Title = title;
        Assert.That(_alias.Validate, Throws.TypeOf<ValidationException>());
    }
}