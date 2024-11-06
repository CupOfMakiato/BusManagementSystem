using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class Bus
{
    public int BusId { get; set; }

    public int? BusNumber { get; set; }

    public int? DriverId { get; set; }

    public int? Status { get; set; }

    public int? AssignedRouteId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Route? AssignedRoute { get; set; }
    public virtual Driver? Driver { get; set; }
}
