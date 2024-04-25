using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace BusShuttleWeb.Models
{
    public class DriverLoopViewModel
    {
        public List<Loop> Loops { get; set; }
        public List<Bus> Busses { get; set; }

        public static DriverLoopViewModel FromData(List<Loop> loops, List<Bus> busses)
        {
            return new DriverLoopViewModel
            {
                Loops = loops,
                Busses = busses,
            };
        }
    }
}