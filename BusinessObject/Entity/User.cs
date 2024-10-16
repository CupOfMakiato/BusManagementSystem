﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Entity;

public partial class User
{
    public int UserId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    public int? RoleId { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string? PhoneNumber { get; set; }   

    public string? Password { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
