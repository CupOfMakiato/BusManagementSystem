using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemDAO
{
    public class BusDAO
    {
        private static BusDAO instance = null;

        public BusDAO()
        {
        }

        public static BusDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new BusDAO();
            }
            return instance;
        }

        public async Task<List<Bus>> GetAllBusesAsync()
        {
            var list = new List<Bus>();
            try
            {
                using var context = new BusManagementSystemContext();
                list = await context.Buses
                    .Include(b => b.Driver)
                    .Include(b => b.AssignedRoute)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public async Task<Bus> GetBusByIdAsync(int busId)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                return await context.Buses
                    .Include(b => b.Driver)
                    .Include(b => b.AssignedRoute)
                    .FirstOrDefaultAsync(b => b.BusId == busId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddBusAsync(Bus bus)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                await context.Buses.AddAsync(bus);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateBusAsync(Bus bus)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                context.Entry(bus).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteBusAsync(Bus bus)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                var existingBus = await context.Buses.SingleOrDefaultAsync(b => b.BusId == bus.BusId);
                if (existingBus != null)
                {
                    context.Buses.Remove(existingBus);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
