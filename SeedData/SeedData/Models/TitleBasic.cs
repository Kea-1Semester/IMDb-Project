using System;
using System.Collections.Generic;

namespace SeedData.Models;

public partial class TitleBasic
{
    public string Tconst { get; set; } = null!;

    public string TitleType { get; set; } = null!;

    public string PrimaryTitle { get; set; } = null!;

    public string OriginalTitle { get; set; } = null!;

    public sbyte IsAdult { get; set; }

    public int StartYear { get; set; }

    public int? EndYear { get; set; }

    public int? RuntimeMinutes { get; set; }

    public virtual ICollection<TitleAkas> TitleAkas { get; set; } = new List<TitleAkas>();

    public virtual ICollection<TitleComment> TitleComments { get; set; } = new List<TitleComment>();

    public virtual ICollection<TitleEpisode> TitleEpisodeParentTconstNavigations { get; set; } = new List<TitleEpisode>();

    public virtual ICollection<TitleEpisode> TitleEpisodeTconstNavigations { get; set; } = new List<TitleEpisode>();

    public virtual ICollection<TitleRating> TitleRatings { get; set; } = new List<TitleRating>();

    public virtual ICollection<TitleGenre> IdGenres { get; set; } = new List<TitleGenre>();
}
