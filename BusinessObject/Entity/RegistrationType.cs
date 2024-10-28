using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class RegistrationType
{
    public int RegistrationTypeId { get; set; }

    public string? TypeName { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
