using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleWeb.Models
{
    public class StopEditModel
    {
       
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string Name {get; set;}


        public static StopEditModel FromStop(Stop stop)
        {
            return new StopEditModel
            {
                Id = stop.Id,
                Name = stop.Name,
            };
        }
    }
}