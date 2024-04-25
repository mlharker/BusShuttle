using System.Collections.Generic;
using DomainModel;

namespace BusShuttleWeb.Models
{
    public class EntryCreateModel
    {
        public int SelectedStopId { get; set; }
        public List<Routes> Routes { get; set; }

        public List<Stop> Stops { get; set; }
        public int Boarded { get; set; }
        public int LeftBehind { get; set; }
        public int LoopId { get; set; }
        public int DriverId { get; set; }
        public int BusId { get; set; }

        public static EntryCreateModel FromData(List<Stop> stops, List<Routes> routes, int loop, int bus, int driver)
        {
            return new EntryCreateModel
            {
                Stops = stops,
                Routes = routes,
                Boarded = 0,
                LeftBehind = 0,
                LoopId = loop,
                BusId = bus,
                DriverId = driver
            };
        }

    }
}