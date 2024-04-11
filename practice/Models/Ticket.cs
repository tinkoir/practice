using System;
using System.Collections.Generic;

namespace practice.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public DateOnly CreationDate { get; set; }

    public DateOnly ClosingDate { get; set; }

    public string Device { get; set; } = null!;

    public string ProblemType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int EngineerId { get; set; }

    public string TicketStatus { get; set; } = null!;

    public string Client { get; set; } = null!;

    public virtual User Engineer { get; set; } = null!;
}
