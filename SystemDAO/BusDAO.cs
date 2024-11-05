using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public List<Bus> GetAllBuses()
        {
            var list = new List<Bus>();
            try
            {
                using var context = new BusManagementSystemContext();
                list = context.Buses
                    .Include(b => b.Driver)
                    .Include(b => b.AssignedRoute)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Bus GetBusById(int busId)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                return context.Buses
                    .Include(b => b.Driver)
                    .Include(b => b.AssignedRoute)
                    .FirstOrDefault(b => b.BusId == busId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddBus(Bus bus)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                context.Buses.Add(bus);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBus(Bus bus)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                context.Entry(bus).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteBus(Bus bus)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                var existingBus = context.Buses.SingleOrDefault(b => b.BusId == bus.BusId);
                if (existingBus != null)
                {
                    context.Buses.Remove(existingBus);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool BusExists(int busId)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                return context.Buses.Any(b => b.BusId == busId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CheckBusNumberExists(int busNumber, int busId = 0)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                return context.Buses.Any(b => b.BusNumber == busNumber && b.BusId != busId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
