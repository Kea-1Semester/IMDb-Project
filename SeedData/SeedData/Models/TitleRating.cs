namespace SeedData.Models;

public partial class TitleRating
{
    public int IdRating { get; set; }

    public string Tconst { get; set; } = null!;

    public double AverageRating { get; set; }

    public int NumVotes { get; set; }

    public virtual TitleBasic TconstNavigation { get; set; } = null!;
}
