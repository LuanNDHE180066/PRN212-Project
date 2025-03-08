using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Invoice
{
    public int IId { get; set; }

    public DateOnly? InvoiceDate { get; set; }

    public decimal? Total { get; set; }

    public int? CustomerId { get; set; }

    public int? StaffId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<HistoryBuyGood> HistoryBuyGoods { get; set; } = new List<HistoryBuyGood>();

    public virtual ICollection<HistoryUsedDevice> HistoryUsedDevices { get; set; } = new List<HistoryUsedDevice>();

    public virtual Staff? Staff { get; set; }
}
