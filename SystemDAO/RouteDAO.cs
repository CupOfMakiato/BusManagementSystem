using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;

namespace SystemDAO
{
    public class RouteDAO
    {
        private static RouteDAO instance = null;

        private RouteDAO()
        { }

        public static RouteDAO getInstance()
        {
            if (instance == null)
            {
                instance = new RouteDAO();
            }
            return instance;
        }

        public List<Route> GetAllRoutes()
        {
            var list = new List<Route>();
            try
            {
                using var db = new BusManagementSystemContext();
                list = db.Routes
                    .Include(r => r.BusStops)
                    .ToList();
            }
            catch (Exception ex)
            {
                return list;
            }
            return list;
        }

        public Route GetRouteById(int routeId)
        {
            using var db = new BusManagementSystemContext();
            return db.Routes.FirstOrDefault(x => x.RouteId == routeId);
        }

        public void AddRoute(Route route)
        {
            try
            {
                using var db = new BusManagementSystemContext();
                db.Routes.Add(route);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRoute(Route route)
        {
            try
            {
                using var db = new BusManagementSystemContext();
                db.Entry<Route>(route).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRoute(Route route)
        {
            try
            {
                using var db = new BusManagementSystemContext();
                var routeToDelete = db.Routes.SingleOrDefault(x => x.RouteId == route.RouteId);
                if (routeToDelete != null)
                {
                    db.Routes.Remove(routeToDelete);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}