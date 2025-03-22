using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class WorkingHistory
{
    public int? StaffId { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public int Id { get; set; }

    public virtual Staff? Staff { get; set; }
}
