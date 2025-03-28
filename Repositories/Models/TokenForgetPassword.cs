using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class TokenForgetPassword
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiryTime { get; set; }

    public bool IsUsed { get; set; }

    public int? Cid { get; set; }

    public virtual Customer? CidNavigation { get; set; }
}
