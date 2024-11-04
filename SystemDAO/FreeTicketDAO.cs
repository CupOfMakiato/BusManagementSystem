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
        public async Task AddFreeTicketAsync(FreeTicket freeTicket)
        {
            if (freeTicket == null)
            {
                throw new ArgumentNullException(nameof(freeTicket), "FreeTicket cannot be null.");
            }

            try
            {
                using var context = new BusManagementSystemContext();
                await context.FreeTickets.AddAsync(freeTicket);
                await context.SaveChangesAsync(); // Đảm bảo lưu thay đổi
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"An error occurred while updating the database: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw new Exception("An error occurred while adding the free ticket.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
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
    }
}
