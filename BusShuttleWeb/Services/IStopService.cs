using DomainModel;

namespace BusShuttleWeb.Services
{
    public interface IStopService
    {
        public List<Stop> getAllStops();

        public List<Stop> getActiveStops();

        public Stop findStopById(int id);

        public void UpdateStopById(int id, string name);

        public int GetLastStopId();

        public void CreateNewStop(int id, string name, double lat, double lon);

        public void deactivateStop(int id);

        public void deleteStop(int id);

    }

}