using DomainModel;

namespace BusShuttleWeb.Services
{
    public interface IBusService
    {
        public List<Bus> getAllBusses();

        public List<Bus> getActiveBusses();

        public Bus findBusById(int id);

        public void UpdateBusById(int id, string name);
        
        public void deactivateBus(int id);

        public int getLastIdNumber();

        public void CreateNewBus(int id, string name);
    }

}