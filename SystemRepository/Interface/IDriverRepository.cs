using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

