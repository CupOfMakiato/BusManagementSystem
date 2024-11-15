using System;
using System.Collections.Generic;

namespace BusinessObject.Entity;

public partial class BusStop
{
    public int StopId { get; set; }

    public string? StopName { get; set; }

    public string? Location { get; set; }

    public virtual RouteBusStop? RouteBusStop { get; set; }
}
