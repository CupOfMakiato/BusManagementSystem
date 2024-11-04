using BusinessObject.Entity;

namespace SystemService
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();

        Ticket GetTicketById(int ticketId);

        void AddTicket(Ticket ticket);

        void UpdateTicket(Ticket ticket);

        void DeleteTicket(int ticketId);

        Ticket CalculateTicketPrice(Ticket ticket);

        List<Route> GetAllRoutes();
    }
}