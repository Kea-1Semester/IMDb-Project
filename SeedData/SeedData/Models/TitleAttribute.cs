using System;
using System.Collections.Generic;

namespace SeedData.Models;

public partial class TitleAttribute
{
    public int IdAttribute { get; set; }

    public string Attribute { get; set; } = null!;

    public virtual ICollection<TitleAkas> IdAkas { get; set; } = new List<TitleAkas>();
}
