namespace BusinessObject.Entity;

public partial class BusStop
{
    public int StopId { get; set; }

    public string? StopName { get; set; }

    public string? Location { get; set; }

    public int? RouteId { get; set; }

    public int? StopOrder { get; set; }

    public virtual Route? Route { get; set; }
}