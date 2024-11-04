using BusinessObject.Entity;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class DriverRepository : IDriverRepository
    {
        public List<Driver> GetAllDrivers()
        {
            return DriverDAO.GetInstance().GetAllDrivers();
        }

        public Driver GetDriverById(int driverId)
        {
            return DriverDAO.GetInstance().GetDriverById(driverId);
        }

        public void AddDriver(Driver driver)
        {
            DriverDAO.GetInstance().AddDriver(driver);
        }

        public void UpdateDriver(Driver driver)
        {
            DriverDAO.GetInstance().UpdateDriver(driver);
        }

        public void DeleteDriver(int driverId)
        {
            DriverDAO.GetInstance().DeleteDriver(driverId);
        }
    }
}