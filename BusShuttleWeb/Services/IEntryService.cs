using DomainModel;

namespace BusShuttleWeb.Services
{
	public interface IEntryService
	{
		public List<Entry> getAllEntries();

		public Entry findEntryById(int id);

		public void createNewEntry(DateTime timeStamp, int boarded, int leftBehind, int busId, int driverId, int loopId, int stopId);

		public Entry getEntryForLoopBusDriver(int loopId, int busId, int driverId);
	}

}