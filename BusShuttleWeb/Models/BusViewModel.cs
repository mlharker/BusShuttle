using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleWeb.Models
{
    public class BusViewModel
    {
        public int Id { get; set;}

        public string BusName {get; set;}        

        public static BusViewModel FromBus(Bus bus)
        {
            return new BusViewModel
            {
                Id = bus.Id,
                BusName = bus.Name
            };
        } 
    }
}