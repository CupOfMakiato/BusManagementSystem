using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class FreeTicketVerification
{
    public int VerificationId { get; set; }

    public int? UserId { get; set; }

    public string? VerificationImage { get; set; }

    public DateTime? VerificationDate { get; set; }

    public int? Status { get; set; }

    public virtual User? User { get; set; }
    //public int? FreeticketId { get; set; }
}
