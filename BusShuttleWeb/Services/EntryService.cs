using DomainModel;
using Microsoft.Extensions.Logging;
namespace BusShuttleWeb.Services
{
    public class EntryService : IEntryService
    {
        private readonly ILogger<EntryService> logger;
        private DataContext db;
        List<Entry> entries;

        public EntryService(ILogger<EntryService> logger)
        {
            this.logger = logger;
        }

        public List<Entry> getAllEntries()
        {
            logger.LogInformation("Getting all entries...");
            db = new DataContext();
            entries = db.Entry
                .Select(e => new Entry(e.TimeStamp, e.Boarded, e.LeftBehind, e.LoopId, e.DriverId, e.StopId, e.BusId)).ToList();
            return entries;
        }

        public Entry findEntryById(int id)
        {
            logger.LogInformation("Finding entry by ID: {Id}", id);
            db = new DataContext();
            var entry = db.Entry
                .SingleOrDefault(e =>e.Id == id);

            return new Entry(entry);
        }

        public void createNewEntry(DateTime timeStamp, int boarded, int leftBehind, int busId, int driverId, int loopId, int stopId)
        {
            logger.LogInformation("Creating new entry...");
            db = new DataContext();
            var totalEntries = db.Entry.Count();
            db.Add(new Entry{Id = totalEntries+1, TimeStamp=timeStamp, Boarded=boarded, LeftBehind=leftBehind, 
                BusId=busId, DriverId=driverId, LoopId=loopId, StopId=stopId});
            db.SaveChanges();
        }


        public Entry getEntryForLoopBusDriver(int loopId, int busId, int driverId)
        {
            logger.LogInformation("Getting entry with loop Id: {LoopId}, bus Id: {BusId} and driver Id: {DriverId}", loopId, busId, driverId);
            db = new DataContext();
            return db.Entry.FirstOrDefault(e => e.LoopId == loopId && e.BusId == busId && e.DriverId == driverId);
        }

    }
}