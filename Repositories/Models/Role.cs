using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Role
{
    public int Rid { get; set; }

    public string? RName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
