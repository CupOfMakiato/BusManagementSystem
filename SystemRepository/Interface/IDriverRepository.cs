using BusinessObject.Entity;

namespace SystemRepository.Interface
{
    public interface IDriverRepository
    {
        List<Driver> GetAllDrivers();

        Driver GetDriverById(int driverId);

        void AddDriver(Driver driver);

        void UpdateDriver(Driver driver);

        void DeleteDriver(int driverId);
    }
}