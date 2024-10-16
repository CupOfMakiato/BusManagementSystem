using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRepository.Interface
{
    public interface IBusRepository
    {
        Task<List<Bus>> GetAllBuses();
        Task<Bus> GetBusById(int busId);
        Task AddBus(Bus bus);
        Task UpdateBus(Bus bus);
        Task DeleteBus(Bus bus);

    }
}
