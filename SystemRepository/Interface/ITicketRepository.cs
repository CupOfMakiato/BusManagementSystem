using BusinessObject.Entity;

namespace SystemRepository
{
    public interface ITicketRepository
    {
        List<Ticket> GetAllTickets();

        Ticket GetTicketById(int ticketId);
        List<Ticket> GetTicketByUserId(int userId);

        void AddTicket(Ticket ticket);

        void UpdateTicket(Ticket ticket);

        void DeleteTicket(int ticketId);
    }
}