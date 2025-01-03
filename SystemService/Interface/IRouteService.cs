﻿using BusinessObject.Entity;

namespace SystemService.Interface
{
    public interface IRouteService
    {
        void AddRoute(Route route);

        void UpdateRoute(Route route);

        void DeleteRoute(Route route);

        Route GetRouteById(int routeId);

        List<Route> GetAllRoutes();
    }
}