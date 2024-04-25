using DomainModel;
using Microsoft.Extensions.Logging;
using System.Linq;
namespace BusShuttleWeb.Services
{
    public class BusService : IBusService
    {
        private readonly ILogger<BusService> logger;
        private DataContext db;
        List<Bus> busses;


        public BusService(ILogger<BusService> logger)
        { 
            this.logger = logger;
        }

        public List<Bus> getAllBusses()
        {
            logger.LogInformation("Getting all busses...");
            db = new DataContext();
            busses = db.Bus
                .Select(b => new Bus(b.Id, b.Name)).ToList();
            return busses;
        }

        public List<Bus> getActiveBusses()
        {
            logger.LogInformation("Getting active busses...");
            db = new DataContext();
            return db.Bus.Where(bus => bus.IsActive).ToList();
        }

        public Bus findBusById(int id)
        {
            logger.LogInformation("Finding bus by ID: {Id}", id);
            db = new DataContext();
            var bus = db.Bus
                .SingleOrDefault(bus =>bus.Id == id);

            return new Bus(bus);
        }

        public void UpdateBusById(int id, string name)
        {
            logger.LogInformation("Updating bus with ID: {Id}", id);
            db = new DataContext();
            var existingBus = db.Bus.SingleOrDefault(bus => bus.Id == id);
            existingBus.Update(name);

            var bus = db.Bus
                .SingleOrDefault(bus => bus.Id == existingBus.Id);
            
            if(bus != null)
            {
                bus.Name = name;
                db.SaveChanges();
            }
        }

        public void deactivateBus(int id)
        {
            logger.LogInformation("Deactivating bus with ID: {Id}", id);
            db = new DataContext();
            var existingBus = db.Bus.FirstOrDefault(b => b.Id == id);
        
            if (existingBus != null)
            {
                db.Bus.Remove(existingBus);
                db.SaveChanges();
            }
        }

        public int getLastIdNumber()
        {
            logger.LogInformation("Getting total amount of busses...");
            db = new DataContext();
            if (db.Bus.Count() > 0 )
            {
                return db.Bus.Max(x => x.Id);
            }
            else
            {
                return 0;
            }
            
        }

        public void CreateNewBus(int id, string name)
        {
            logger.LogInformation("Creating new bus with ID: {Id}", id);
            db = new DataContext();
            db.Add(new Bus{Id=id, Name=name});
            db.SaveChanges();
        }
    }
}