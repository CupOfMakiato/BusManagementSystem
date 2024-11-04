using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;

namespace SystemDAO
{
    public class DriverDAO
    {
        private static DriverDAO instance = null;

        private DriverDAO()
        { }

        public static DriverDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new DriverDAO();
            }
            return instance;
        }

        public List<Driver> GetAllDrivers()
        {
            using var context = new BusManagementSystemContext();
            return context.Drivers.Include(d => d.Role).ToList();
        }

        public Driver GetDriverById(int driverId)
        {
            using var context = new BusManagementSystemContext();
            return context.Drivers.FirstOrDefault(d => d.DriverId == driverId);
        }

        public void AddDriver(Driver driver)
        {
            try
            {
                using var context = new BusManagementSystemContext();
                context.Drivers.Add(driver);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Xử lý lỗi
                throw new Exception("An error occurred while adding the driver.", ex);
            }
        }

        public void UpdateDriver(Driver driver)
        {
            using var context = new BusManagementSystemContext();
            context.Entry(driver).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteDriver(int driverId)
        {
            using var context = new BusManagementSystemContext();
            var driver = context.Drivers.Find(driverId);
            if (driver != null)
            {
                context.Drivers.Remove(driver);
                context.SaveChanges();
            }
        }
    }
}