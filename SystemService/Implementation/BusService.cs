using BusinessObject.Entity;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;

        public BusService(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public void AddBus(Bus bus)
        {
            _busRepository.AddBus(bus);
        }

        public void DeleteBus(Bus bus)
        {
            _busRepository.DeleteBus(bus);
        }

        public List<Bus> GetAllBuses()
        {
            return _busRepository.GetAllBuses();
        }

        public Bus GetBusById(int busId)
        {
            return _busRepository.GetBusById(busId);
        }

        public void UpdateBus(Bus bus)
        {
            _busRepository.UpdateBus(bus);
        }
    }
}