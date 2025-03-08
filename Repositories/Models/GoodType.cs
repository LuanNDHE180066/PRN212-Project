using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class GoodType
{
    public int Gtid { get; set; }

    public string? GtName { get; set; }

    public virtual ICollection<Good> Goods { get; set; } = new List<Good>();
}
