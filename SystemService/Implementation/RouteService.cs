using BusinessObject.Entity;
using SystemRepository.Interface;
using SystemService.Interface;
using System.Collections.Generic;

namespace SystemService.Implementation
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public void AddRoute(Route route)
        {
            _routeRepository.AddRoute(route);
        }

        public void UpdateRoute(Route route)
        {
            _routeRepository.UpdateRoute(route);
        }

        public void DeleteRoute(Route route)
        {
            _routeRepository.DeleteRoute(route);
        }

        public Route GetRouteById(int routeId)
        {
            return _routeRepository.GetRouteById(routeId);
        }

        public List<Route> GetAllRoutes()
        {
            return _routeRepository.GetAllRoutes();
        }
    }
}
