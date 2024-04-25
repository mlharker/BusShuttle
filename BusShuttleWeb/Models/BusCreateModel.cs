using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleWeb.Models
{
    public class BusCreateModel
    {
       
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        public string BusName {get; set;}

        public static BusCreateModel NewBus(int lastId)
        {
            var newId = lastId + 1;

            return new BusCreateModel
            {
                Id = newId,
                BusName = "",
            };
        }
    }
}