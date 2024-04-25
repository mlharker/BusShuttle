using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleWeb.Models
{
    public class EditBusModel
    {
       
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string BusName {get; set;}


        public static EditBusModel FromBus(Bus bus)
        {
            return new EditBusModel
            {
                Id = bus.Id,
                BusName = bus.Name,
            };
        }
    }
}