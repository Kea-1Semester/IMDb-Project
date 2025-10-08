using System;
using System.Collections.Generic;

namespace SeedData.Models;

public partial class TitleAkas
{
    public int IdAkas { get; set; }

    public string Tconst { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Language { get; set; } = null!;

    public sbyte IsOriginalTitle { get; set; }

    public virtual TitleBasic TconstNavigation { get; set; } = null!;

    public virtual ICollection<TitleAttribute> IdAttributes { get; set; } = new List<TitleAttribute>();

    public virtual ICollection<TitleType> IdTypes { get; set; } = new List<TitleType>();
}
