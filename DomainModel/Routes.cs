using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
     public class Routes
     {
          [Key]
          public int Id {get; set;} 

          public int Order {get; set;}

          public int LoopId {get; set;}

          public Loop Loop {get; set;}

          public int StopId {get; set;}

          public Stop Stop {get; set;}

          public Routes()
          {
               
          }

          public Routes(int id, int order)
          {
               Id = id;
               Order = order;
          }

          public Routes(int order)
          {
               Order = order;
          }

          public Routes(Routes Route)
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