using System;
using System.Collections.Generic;

namespace SeedData.Models;

public sealed partial class TitleType
{
    public int IdTypes { get; set; }

    public string Type { get; set; } = null!;

    public ICollection<TitleAkas> IdAkas { get; set; } = new List<TitleAkas>();
}
