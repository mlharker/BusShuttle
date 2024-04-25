using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
     public class Route
     {
          [Key]
          public int Id {get; set;} 

          public int Order {get; set;}

          public int LoopId {get; set;}

          public Loop Loop {get; set;}

          public int StopId {get; set;}

          public Stop Stop {get; set;}

          public Route()
          {
               
          }

          public Route(int id, int order)
          {
               Id = id;
               Order = order;
          }

          public Route(int order)
          {
               Order = order;
          }

          public Route(Route Route)
          {
               Id = Route.Id;
               Order = Route.Order;
          }

          public void Update(int order)
          {
               Order = order;
          }
     }

}