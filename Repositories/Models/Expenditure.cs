using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Expenditure
{
    public int ExId { get; set; }

    public int? GoodsId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Total { get; set; }

    public DateOnly? ExDate { get; set; }

    public string? Supplier { get; set; }

    public int? StaffId { get; set; }

    public virtual Good? Goods { get; set; }

    public virtual Staff? Staff { get; set; }
}
