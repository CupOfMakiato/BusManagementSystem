using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? BookingId { get; set; }

    public int? UserId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? DateTime { get; set; }

    public int? Status { get; set; }

    public int? RegistrationTypeId { get; set; }

    public bool? IsFreeTicket { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual ICollection<FreeTicket> FreeTickets { get; set; } = new List<FreeTicket>();

    public virtual RegistrationType? RegistrationType { get; set; }

    public virtual ICollection<RouteTicket> RouteTickets { get; set; } = new List<RouteTicket>();

    public virtual User? User { get; set; }
}
