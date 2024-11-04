using BusinessObject.Entity;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public List<Driver> GetAllDrivers()
        {
            return _driverRepository.GetAllDrivers();
        }

        public Driver GetDriverById(int driverId)
        {
            return _driverRepository.GetDriverById(driverId);
        }

        public void AddDriver(Driver driver)
        {
            _driverRepository.AddDriver(driver);
        }

        public void UpdateDriver(Driver driver)
        {
            _driverRepository.UpdateDriver(driver);
        }

        public void DeleteDriver(int driverId)
        {
            _driverRepository.DeleteDriver(driverId);
        }
    }
}