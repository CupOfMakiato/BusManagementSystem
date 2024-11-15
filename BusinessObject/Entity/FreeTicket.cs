using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class FreeTicket
{
    public int FreeTicketId { get; set; }

    public int TicketId { get; set; }

    public string RecipientName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Idnumber { get; set; } = null!;

    public byte[]? IdfrontImage { get; set; }

    public byte[]? IdbackImage { get; set; }

    public string District { get; set; } = null!;

    public string Ward { get; set; } = null!;

    public string RecipientType { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public byte[]? Portrait3x4Image { get; set; }

    public byte[]? ProofFrontImage { get; set; }

    public byte[]? ProofBackImage { get; set; }

    public string TicketDeliveryAddress { get; set; } = null!;

    public DateOnly IssueDate { get; set; }

    public DateOnly? ValidUntil { get; set; }

    public int? Status { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}
