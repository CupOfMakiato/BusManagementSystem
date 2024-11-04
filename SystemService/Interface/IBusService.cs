using BusinessObject.Entity;

namespace SystemService.Interface
{
    public interface IBusService
    {
        List<Bus> GetAllBuses();

        Bus GetBusById(int busId);

        void AddBus(Bus bus);

        void UpdateBus(Bus bus);

        void DeleteBus(Bus bus);
    }
}