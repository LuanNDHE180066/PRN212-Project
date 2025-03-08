using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class HistoryUsedDevice
{
    public int HudId { get; set; }

    public int? InvoiceId { get; set; }

    public int? DeviceId { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? Start { get; set; }

    public TimeOnly? End { get; set; }

    public decimal? Amount { get; set; }

    public virtual Device? Device { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
