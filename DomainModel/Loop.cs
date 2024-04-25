namespace DomainModel;
using System.ComponentModel.DataAnnotations;

public class Loop
{
     public int Id { get; set;}

     public string Name {get; set;}

     public bool IsActive { get; set; }

     public List<Routes> Routes { get; set;}

     public Loop()
     {
          IsActive = true;
     }

     public Loop(int id, string name)
     {
          Id = id;
          Name = name;
     }

     public Loop(string name)
     {
          Name = name;
     }

     public Loop(Loop loop)
     {
          Id = loop.Id;
          Name = loop.Name;
     }

     public void Update(string name)
     {
          Name = name;
     }
}