using System.ComponentModel.DataAnnotations;

namespace SeedData.Models;

public partial class NameBasic
{
    [MaxLength(100)]
    public string? Nconst { get; set; }

    public string? PrimaryName { get; set; }

    public int? BirthYear { get; set; }

    public int? DeathYear { get; set; }
}
