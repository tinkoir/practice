using System;
using System.Collections.Generic;

namespace practice.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
