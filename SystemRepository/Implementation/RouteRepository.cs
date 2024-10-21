using BusinessObject.Entity;
using SystemDAO;
using SystemRepository.Interface;
using System.Collections.Generic;

namespace SystemRepository.Implementation
{
    public class RouteRepository : IRouteRepository
    {
        public void AddRoute(Route route)
        {
            RouteDAO.getInstance().AddRoute(route);
        }

        public void DeleteRoute(Route route)
        {
            RouteDAO.getInstance().DeleteRoute(route);
        }

        public Route GetRouteById(int routeId)
        {
            return RouteDAO.getInstance().GetRouteById(routeId);
        }

        public List<Route> GetAllRoutes()
        {
            return RouteDAO.getInstance().GetAllRoutes();
        }

        public void UpdateRoute(Route route)
        {
            RouteDAO.getInstance().UpdateRoute(route);
        }
    }
}
