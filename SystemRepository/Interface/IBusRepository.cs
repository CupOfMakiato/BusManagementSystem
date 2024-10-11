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
        List<Bus> GetAllBuses();
        Bus GetBusById(int busId);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(Bus bus);

    }
}
