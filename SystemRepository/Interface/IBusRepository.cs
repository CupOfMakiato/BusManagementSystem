using BusinessObject.Entity;

namespace SystemRepository.Interface
{
    public interface IBusRepository
    {
        List<Bus> GetAllBuses();

        Bus GetBusById(int busId);

        void AddBus(Bus bus);

        void UpdateBus(Bus bus);

        void DeleteBus(Bus bus);
        bool BusExists(int busId);
        bool CheckBusNumberExists(int busNumber, int busId = 0);
    }
}