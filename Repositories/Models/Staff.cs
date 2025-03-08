using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Staff
{
    public int Sid { get; set; }

    public int? Roleid { get; set; }

    public string? Phone { get; set; }

    public string? SName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Address { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Expenditure> Expenditures { get; set; } = new List<Expenditure>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Role? Role { get; set; }
}
