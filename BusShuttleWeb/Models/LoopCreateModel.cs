using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleWeb.Models
{
    public class LoopCreateModel
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        public static LoopCreateModel NewLoop(int amountOfLoops)
        {
            var newId = amountOfLoops + 1;

            return new LoopCreateModel
            {
                Id = newId,
                Name = "",
            };
        }
    }
}