using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int? RoleId { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    //[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    //[DataType(DataType.Password)]
    //[RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
    //    ErrorMessage = "Password must be at least 6 characters long, contain at least one uppercase letter, one number, and one special character.")]
    //public string Password { get; set; } = string.Empty;
    public string Password { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<FreeTicketVerification> FreeTicketVerifications { get; set; } = new List<FreeTicketVerification>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
