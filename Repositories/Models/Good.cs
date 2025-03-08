using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Good
{
    public int Gid { get; set; }

    public string? GName { get; set; }

    public int? Typeid { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Unit { get; set; }

    public int? Status { get; set; }

    public string? Img { get; set; }

    public virtual ICollection<Expenditure> Expenditures { get; set; } = new List<Expenditure>();

    public virtual ICollection<HistoryBuyGood> HistoryBuyGoods { get; set; } = new List<HistoryBuyGood>();

    public virtual GoodType? Type { get; set; }
}
