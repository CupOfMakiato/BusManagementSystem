using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class BusService :IBusService
    {
        private readonly IBusRepository _busRepository;
        public BusService(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public async Task AddBus(Bus bus)
        {
            await _busRepository.AddBus(bus);
        }

        public async Task DeleteBus(Bus bus)
        {
            await _busRepository.DeleteBus(bus);
        }

        public async Task<List<Bus>> GetAllBuses()
        {
            return await _busRepository.GetAllBuses();
        }

        public async Task<Bus> GetBusById(int busId)
        {
            return await _busRepository.GetBusById(busId);
        }

        public async Task UpdateBus(Bus bus)
        {
            await _busRepository.UpdateBus(bus);
        }
    }
}
