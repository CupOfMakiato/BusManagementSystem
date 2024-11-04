using BusinessObject.Entity;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class BusRepository : IBusRepository
    {
        public void AddBus(Bus bus)
        {
            BusDAO.GetInstance().AddBus(bus);
        }

        public void DeleteBus(Bus bus)
        {
            BusDAO.GetInstance().DeleteBus(bus);
        }

        public List<Bus> GetAllBuses()
        {
            return BusDAO.GetInstance().GetAllBuses();
        }

        public Bus GetBusById(int busId)
        {
            return BusDAO.GetInstance().GetBusById(busId);
        }

        public void UpdateBus(Bus bus)
        {
            BusDAO.GetInstance().UpdateBus(bus);
        }
    }
}