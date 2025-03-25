using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Device
{
    public int Did { get; set; }

    public int? Typeid { get; set; }

    public int? Hours { get; set; }

    public int? Running { get; set; }

    public int? Status { get; set; }

    public decimal? PricePerHour { get; set; }

    public virtual ICollection<HistoryUsedDevice> HistoryUsedDevices { get; set; } = new List<HistoryUsedDevice>();

    public virtual DeviceType? Type { get; set; }
}
