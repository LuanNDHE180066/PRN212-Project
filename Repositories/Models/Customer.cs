using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Customer
{
    public int Cid { get; set; }

    public string? CName { get; set; }

    public int? Hours { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
