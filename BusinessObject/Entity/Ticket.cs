using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? UserId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Status { get; set; }

    public bool? IsFreeTicket { get; set; }

    public int? RouteId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<FreeTicket> FreeTickets { get; set; } = new List<FreeTicket>();

    public virtual Route? Route { get; set; }

    public virtual User? User { get; set; }
}
