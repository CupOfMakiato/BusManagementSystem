using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Entity;

public partial class FreeTicketVerification
{
    [Key] // Primary key attribute added to resolve InvalidOperationException
    public int VerificationId { get; set; }

    public int? UserId { get; set; }

    public int? FreeTicketId { get; set; } // Added FreeTicketId to match the updated script

    public string? VerificationImage { get; set; }

    public DateTime? VerificationDate { get; set; }

    public int? Status { get; set; }

    public virtual User? User { get; set; }

    public virtual FreeTicket? FreeTicket { get; set; } // Added navigation property for FreeTicket
}