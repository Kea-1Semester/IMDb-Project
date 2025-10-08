using System;
using System.Collections.Generic;

namespace SeedData.Models;

public partial class TitleComment
{
    public int IdComment { get; set; }

    public string Tconst { get; set; } = null!;

    public string Comment { get; set; } = null!;

    public virtual TitleBasic TconstNavigation { get; set; } = null!;
}
