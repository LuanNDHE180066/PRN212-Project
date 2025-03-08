using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class HistoryBuyGood
{
    public int HbgId { get; set; }

    public int? InvoiceId { get; set; }

    public int? GoodsId { get; set; }

    public DateOnly? Date { get; set; }

    public int? Quantity { get; set; }

    public decimal? Amount { get; set; }

    public virtual Good? Goods { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
