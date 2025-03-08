using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class DeviceType
{
    public int DtId { get; set; }

    public string? DtName { get; set; }

    public string? Detail { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
