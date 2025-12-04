using System.ComponentModel.DataAnnotations;
using EfCoreModelsLib.DTO;

namespace unit.tests;

public class PersonsTest
{
    private PersonsDto _persons;

    [SetUp]
    public void Setup()
    {
        _persons = new PersonsDto()
        {
            Name = "ValidName",
            BirthYear = 1990,
            EndYear = 2020
        };
    }

    [TestCase("Bo")]
    [TestCase("Bob")]
    [TestCase("Aurelianthropis Evermorian Celestinaris")]
    [TestCase("AurelianthropisEvermorianCelestinarisVeloriandremusQuintessariothMagnalorianthysZephyrocalisTrionax")]
    public void ValidNameTest(string name)
    {
        _persons.Name = name;
        Assert.That(_persons.Validate, Throws.Nothing);
    }

    [TestCase(1995)]
    public void ValidBirthYear(int birthYear)
    {
        _persons.BirthYear = birthYear;
        Assert.That(_persons.Validate, Throws.Nothing);
    }

    [TestCase(2020)]
    public void ValidEndYear(int endYear)
    {
        _persons.EndYear = endYear;
        Assert.That(_persons.Validate, Throws.Nothing);
    }

    [TestCase("B")]
    [TestCase("AurelianthropisEvermorianCelestinarisVeloriandremusQuintessariothMagnalorianthysZephyrocalyxAetherone")]
    [TestCase("AurelianthropisEvermorianCelestinarisVeloriandremusQuintessariothMagnalorianthysZephyrocalyxAetheroner")]
    [TestCase("AurelianthropisEvermorianCelestinarisVeloriandremusQuintessariothMagnalorianthysZephyrocalyxAetheronAurelianthropisEvermorianCelestinarisVeloriandremus")]
    public void InValidNameTest(string name)
    {
        _persons.Name = name;
        Assert.That(_persons.Validate, Throws.TypeOf<ValidationException>());
    }

    [TestCase(1)]
    [TestCase(19)]
    [TestCase(199)]
    [TestCase(19999)]
    public void InValidBirthYear(int birthYear)
    {
        _persons.BirthYear = birthYear;
        Assert.That(_persons.Validate, Throws.TypeOf<ValidationException>());
    }

    [TestCase(1)]
    [TestCase(19)]
    [TestCase(199)]
    [TestCase(19999)]
    public void InValidEndYear(int endYear)
    {
        _persons.EndYear = endYear;
        Assert.That(_persons.Validate, Throws.TypeOf<ValidationException>());
    }
}