using BusinessObject.Entity;

namespace SystemService.Interface
{
    public interface IDriverService
    {
        List<Driver> GetAllDrivers();

        Driver GetDriverById(int driverId);

        void AddDriver(Driver driver);

        void UpdateDriver(Driver driver);

        void DeleteDriver(int driverId);
    }
}