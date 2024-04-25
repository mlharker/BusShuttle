using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace BusShuttleWeb.Models
{
    public class EntryViewModel
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public int Boarded { get; set; }

        public int LeftBehind { get; set; }

        public string LoopName { get; set; }

        public string StopName { get; set; }

        public string BusName { get; set; }

        public static EntryViewModel FromEntry(Entry entry, Loop loop, Stop stop, Bus bus)
        {
            return new EntryViewModel
            {
                Id = entry.Id,
                TimeStamp = entry.TimeStamp,
                Boarded = entry.Boarded,
                LeftBehind = entry.LeftBehind,
                LoopName = loop.Name,
                StopName = stop.Name,
                BusName = bus.Name
            };
        }
    }
}