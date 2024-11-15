using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class RouteBusStop
{
    public int StopId { get; set; }

    public int RouteId { get; set; }

    public int? StopOrder { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual BusStop Stop { get; set; } = null!;
}
