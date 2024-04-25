using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;


namespace BusShuttleWeb.Models
{
    public class LoopEditModel
    {

        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        public static LoopEditModel FromLoop(Loop loop)
        {
            return new LoopEditModel
            {
                Id = loop.Id,
                Name = loop.Name,
            };
        }
    }
}