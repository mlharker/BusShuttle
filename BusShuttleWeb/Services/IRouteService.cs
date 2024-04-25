using DomainModel;

namespace BusShuttleWeb.Services
{
    public interface IRouteService
    {
        public List<Routes> getAllRoutes();

        public List<Routes> getRoutesByLoopId(int loopId);

        public Routes findRouteById(int id);

        public void UpdateRouteById(int id, int order);

        public int GetAmountOfRoutes();

        public void CreateNewRoute(int order, int loopId, int stopId);

        public void IncreaseRouteOrder(int id);

        public void DecreaseRouteOrder(int id);

        public void DeleteRoute(int id);
    }

}