using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class BusRepository : IBusRepository
    {
        public async Task AddBus(Bus bus)
        {
            await BusDAO.GetInstance().AddBusAsync(bus);
        }

        public async Task DeleteBus(Bus bus)
        {
            await BusDAO.GetInstance().DeleteBusAsync(bus);
        }

        public async Task<List<Bus>> GetAllBuses()
        {
            return await BusDAO.GetInstance().GetAllBusesAsync();
        }

        public async Task<Bus> GetBusById(int busId)
        {
            return await BusDAO.GetInstance().GetBusByIdAsync(busId);
        }

        public async Task UpdateBus(Bus bus)
        {
            await BusDAO.GetInstance().UpdateBusAsync(bus);
        }
    }
}
