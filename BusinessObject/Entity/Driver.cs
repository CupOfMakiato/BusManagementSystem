using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class Driver
{
    public int DriverId { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Status { get; set; }

    public DateTime? Shift { get; set; }

    public string? Email { get; set; }

    public int? RoleId { get; set; }


    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();

    public virtual Role? Role { get; set; }
}
