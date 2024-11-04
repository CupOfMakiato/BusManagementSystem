namespace BusinessObject.Entity;

public partial class Route
{
    public int RouteId { get; set; }

    public string? RouteName { get; set; }

    public string? StartLocation { get; set; }

    public string? EndLocation { get; set; }

    public decimal? Distance { get; set; }

    public TimeOnly? Duration { get; set; }

    public virtual ICollection<BusStop> BusStops { get; set; } = new List<BusStop>();

    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}