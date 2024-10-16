using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemService.Interface
{
    public interface IBusService
    {
        Task<List<Bus>> GetAllBuses();
        Task<Bus> GetBusById(int busId);
        Task AddBus(Bus bus);
        Task UpdateBus(Bus bus);
        Task DeleteBus(Bus bus);
    }
}
