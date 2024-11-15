using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemDAO
{
    public class FreeTicketDAO
    {
        private static FreeTicketDAO instance = null;

        private FreeTicketDAO() { }

        public static FreeTicketDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new FreeTicketDAO();
            }
            return instance;
        }

        // Lấy tất cả các FreeTicket
        public List<FreeTicket> GetAllFreeTickets()
        {
            using var context = new BusManagementSystemContext();
            return context.FreeTickets.ToList(); // Sử dụng FreeTickets
        }

        // Lấy FreeTicket theo ID
        public FreeTicket GetFreeTicketById(int id)
        {
            using var context = new BusManagementSystemContext();
            return context.FreeTickets.Find(id); // Sử dụng FreeTickets
        }

        // Thêm FreeTicket mới
        public void AddFreeTicket(FreeTicket freeTicket)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                context.FreeTickets.Add(freeTicket);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Xử lý lỗi
                throw new Exception("An error occurred while adding the ticket.", ex);
            }
        }




        // Cập nhật FreeTicket
        public void UpdateFreeTicket(FreeTicket freeTicket)
        {
            using var context = new BusManagementSystemContext();
            context.Entry(freeTicket).State = EntityState.Modified; // Sử dụng FreeTicket
            context.SaveChanges();
        }

        // Xóa FreeTicket
        public void DeleteFreeTicket(int id)
        {
            using var context = new BusManagementSystemContext();
            var freeTicket = GetFreeTicketById(id); // Sử dụng FreeTicket
            if (freeTicket != null)
            {
                context.FreeTickets.Remove(freeTicket); // Sử dụng FreeTickets
                context.SaveChanges();
            }
        }
        //public bool IsIdNumberExistingWithStatus(string idNumber, int status)
        //{
        //    using var context = new BusManagementSystemContext();
        //    // Check if there is an entry with the specified ID number and a status of 0 or 1
        //    return context.FreeTickets.Any(ticket => ticket.Idnumber == idNumber && (ticket.Status == 0 || ticket.Status == 1));
        //}

        public async Task<Ticket> GetOrCreateTicketAsync(string idNumber)
        {
            using var context = new BusManagementSystemContext();
            // Check if a free Ticket with the provided Idnumber exists
            var existingTicket = await context.Tickets
                .FirstOrDefaultAsync(t => t.IsFreeTicket == true && t.FreeTickets.Any(ft => ft.Idnumber == idNumber));

            if (existingTicket != null)
            {
                // Return the existing Ticket if found
                return existingTicket;
            }

            // If no Ticket found, create a new one
            var newTicket = new Ticket
            {
                IsFreeTicket = true,
                Status = 1, // Set initial status as needed (e.g., 1 = Pending)
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(30),
                // Set other properties if needed, such as RouteId, TicketType, etc.
            };

            context.Tickets.Add(newTicket);
            await context.SaveChangesAsync();

            return newTicket;
        }
        public bool IsIdNumberExisting(string idNumber)
        {
            using var context = new BusManagementSystemContext();
            // Check if there is an entry with the specified ID number
            return context.FreeTickets.Any(ticket => ticket.Idnumber == idNumber);
        }
        public bool IsExpired(FreeTicket freeTicket)
        {
            if (freeTicket.ValidUntil <= DateOnly.FromDateTime(DateTime.Today))
            {
                freeTicket.Status = 0; // expired
                using var context = new BusManagementSystemContext();
                context.FreeTickets.Update(freeTicket);
                context.SaveChanges();
                return true; //expired
            }
            return false; //valid
        }

        public void VerifyFreeTicket(FreeTicket freeTicket)
        {
            using var context = new BusManagementSystemContext();

            // set to verified
            freeTicket.Status = 2;
            context.FreeTickets.Update(freeTicket);

            // Set status of associated Ticket to 2 (Verified)
            var ticket = context.Tickets.Find(freeTicket.TicketId);
            if (ticket != null)
            {
                ticket.Status = 2;
                context.Tickets.Update(ticket);
            }

            context.SaveChanges();
        }
    }
}
