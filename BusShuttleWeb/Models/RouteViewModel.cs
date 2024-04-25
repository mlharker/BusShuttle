using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace BusShuttleWeb.Models
{
    public class RouteViewModel
    {
        public List<Loop> Loops { get; set; }
        public List<Routes> Routes { get; set; }
        public Loop SelectedLoop { get; set; }

        public List<Stop> Stops { get; set; }

        public static RouteViewModel FromRoutes(List<Routes> routes, List<Loop> loops)
        {
            return new RouteViewModel
            {
                Loops = loops,
                Routes = routes

            };
        }

        public static RouteViewModel FromLoopID(List<Routes> routes, List<Loop> loops, Loop selectedLoop, List<Stop> stops)
        {
            return new RouteViewModel
            {
                Loops = loops,
                Routes = routes,
                SelectedLoop = selectedLoop,
                Stops = stops
            };
        }
    }
}