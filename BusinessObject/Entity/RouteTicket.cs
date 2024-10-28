using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class RouteTicket
{
    public int RouteTicketId { get; set; }

    public int RouteId { get; set; }

    public int TicketId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
