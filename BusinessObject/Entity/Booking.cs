﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? BusId { get; set; }

    public DateTime? BookingDate { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual Bus? Bus { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual User? User { get; set; }
}
