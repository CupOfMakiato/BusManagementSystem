using BusinessObject.Entity;
using System.Collections.Generic;

namespace SystemRepository.Interface
{
    public interface IRouteRepository
    {
        void AddRoute(Route route);
        void UpdateRoute(Route route);
        void DeleteRoute(Route route);
        Route GetRouteById(int routeId);
        List<Route> GetAllRoutes();
    }
}
