using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleWeb.Models
{
    public class StopCreateModel
    {
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string Name {get; set;}

        public double Latitude {get; set;}

        public double Longitude {get; set;}

        public static StopCreateModel NewStop(int amountOfStops)
        {
            var newId = amountOfStops + 1;

            return new StopCreateModel
            {
                Id = newId,
                Name = "",
                Latitude = 0.0,
                Longitude = 0.0
            };
        }
    }
}