using System;
using System.Collections.Generic;

namespace SeedData.Models;

public partial class TitleGenre
{
    public int IdGenre { get; set; }

    public string Genre { get; set; } = null!;

    public virtual ICollection<TitleBasic> Tconsts { get; set; } = new List<TitleBasic>();
}
