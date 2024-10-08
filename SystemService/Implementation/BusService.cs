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
    }
}
