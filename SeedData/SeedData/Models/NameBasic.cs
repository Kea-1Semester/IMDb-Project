using System.ComponentModel.DataAnnotations;

namespace SeedData.Models;

public partial class NameBasic
{
    [MaxLength(100)]
    public string? Nconst { get; set; }

    public string? PrimaryName { get; set; }

    public double? BirthYear { get; set; }

    public double? DeathYear { get; set; }
}
