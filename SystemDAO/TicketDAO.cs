using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;

namespace SystemDAO
{
    public class TicketDAO
    {
        private static TicketDAO instance = null;

        public TicketDAO()
        {
        }

        public static TicketDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TicketDAO();
                }
                return instance;
            }
        }

        public List<Ticket> GetAllTickets()
        {
            try
            {
                using var context = new BusManagementSystemContext();
                return context.Tickets
                    .Include(t => t.User)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all tickets: " + ex.Message);
            }
        }

        public Ticket GetTicketById(int ticketId)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                return context.Tickets
                    .Include(t => t.User)
                    .FirstOrDefault(t => t.TicketId == ticketId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving ticket by ID: " + ex.Message);
            }
        }

        public void AddTicket(Ticket ticket)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                context.Tickets.Add(ticket);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding ticket: " + ex.Message);
            }
        }

        public void UpdateTicket(Ticket ticket)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                context.Entry(ticket).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating ticket: " + ex.Message);
            }
        }

        public void DeleteTicket(int ticketId)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                var ticket = context.Tickets.Find(ticketId);
                if (ticket != null)
                {
                    context.Tickets.Remove(ticket);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Ticket not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting ticket: " + ex.Message);
            }
        }
    }
}