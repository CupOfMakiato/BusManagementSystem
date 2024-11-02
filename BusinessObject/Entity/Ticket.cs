using System;
using System.Collections.Generic;

namespace BusinessObject.Entity
{
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

        // New properties for priority type and uploaded files
        public string? PriorityType { get; set; } // e.g., "Học Sinh, Sinh Viên", "Công Nhân khu CN"
        public string? Photo3x4 { get; set; } // File path or URL for 3x4 photo
        public string? IDCardFront { get; set; } // File path or URL for ID card front
        public string? IDCardBack { get; set; } // File path or URL for ID card back

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<FreeTicket> FreeTickets { get; set; } = new List<FreeTicket>();
        public virtual Route? Route { get; set; }
        public virtual User? User { get; set; }
    }
}
