using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? BookingId { get; set; }

    public int? UserId { get; set; }

    public double? Price { get; set; }

    public DateTime? DateTime { get; set; }

    public int? Status { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual User? User { get; set; }
}
