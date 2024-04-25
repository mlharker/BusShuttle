using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusShuttleWeb.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BusShuttleWeb.Services;
using DomainModel;

namespace BusShuttleWeb.Conrollers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    IBusService busService;

    public HomeController(ILogger<HomeController> logger, IBusService bService)
    {
        _logger = logger;
        this.busService = bService;
    }

    public IActionResult Index()
    {
        return View();
    }
    [Authorize(Policy = "ManagerRequired")]
    public IActionResult Bus()
    {
        var activeBusses = busService.getAllBusses(); 
        var busViewModels = activeBusses.Select(bus => new BusViewModel
        {
            Id = bus.Id,
            BusName = bus.Name
        }).ToList();
        
        return View(busViewModels);
    }

    [Authorize(Policy = "DriverRequired")]
    public IActionResult Driver()
    {
        return View();
    }
    
    public IActionResult Entry()
    {
        return View();
    }
    [Authorize(Policy = "ManagerRequired")]
    public IActionResult Loop()
    {
        return View();
    }
    [Authorize(Policy = "ManagerRequired")]
    public IActionResult Stop()
    {
        return View();
    }
    [Authorize(Policy = "ManagerRequired")]
    public IActionResult Route()
    {
        return View();
    }
    [Authorize(Policy = "ManagerRequired")]
        public IActionResult Manager()
    {
        return View();
    }

    public IActionResult BusCreate()
    {
        var busCreateModel = BusCreateModel.NewBus(busService.GetAmountOfBusses());
        return View(busCreateModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BusCreate(int id, [Bind("BusName")] BusCreateModel bus)
    {
        busService.CreateNewBus(id, bus.BusName);
        return RedirectToAction("Bus");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
