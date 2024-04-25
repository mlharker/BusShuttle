﻿namespace DomainModel;
using System.ComponentModel.DataAnnotations;

public class Bus
{
    [Key]
    public int Id {get; set;}
    public string Name {get; set;}
    public bool IsActive {get; set;}

    public Bus()
    {
        IsActive = true;
    }

    public Bus(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public Bus(string name)
    {
        Name = name;
    }

    public Bus(Bus bus)
    {
        Id = bus.Id;
        Name = bus.Name;
    }

    public void Update(string name)
    {
        Name = name;
    }
}
