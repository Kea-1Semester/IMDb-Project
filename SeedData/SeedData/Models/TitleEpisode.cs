using System;
using System.Collections.Generic;

namespace SeedData.Models;

public partial class TitleEpisode
{
    public string Tconst { get; set; } = null!;

    public string ParentTconst { get; set; } = null!;

    public int SeasonNumber { get; set; }

    public int EpisodeNumber { get; set; }

    public virtual TitleBasic ParentTconstNavigation { get; set; } = null!;

    public virtual TitleBasic TconstNavigation { get; set; } = null!;
}
