using DomainModel;

namespace BusShuttleWeb.Services
{
    public interface ILoopService
    {
        public List<Loop> getAllLoops();

        public List<Loop> getActiveLoops();

        public Loop getLoopById(int id);

        public void UpdateLoopById(int id, string name);

        public void deactivateLoop(Loop loop);

        public int GetAmountOfLoops();

        public void CreateNewLoop(int id, string name);
    }

}