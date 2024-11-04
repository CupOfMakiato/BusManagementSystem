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
        public string? TicketType { get; set; } // Added TicketType property
        public string? PriorityType { get; set; }
        public bool? IsPriority { get; set; } // Added IsPriority property
        public byte[]? Photo3x4 { get; set; }
        public byte[]? IDCardFront { get; set; }
        public byte[]? IDCardBack { get; set; }
        public virtual Booking? Booking { get; set; }
        public virtual ICollection<FreeTicket> FreeTickets { get; set; } = new List<FreeTicket>();
        public virtual Route? Route { get; set; }
        public virtual User? User { get; set; }
    }
}