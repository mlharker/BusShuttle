using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BusShuttleWeb.Models;
using DomainModel;
using BusShuttleWeb.Services;

namespace BusShuttleWeb.Conrollers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    IBusService busService;
    IStopService stopService;

    public HomeController(ILogger<HomeController> logger, IBusService bService, IStopService stService)
    {
        _logger = logger;
        this.busService = bService;
        this.stopService = stService;
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
        var busCreateModel = BusCreateModel.NewBus(busService.getLastIdNumber());
        return View(busCreateModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BusCreate(int id, [Bind("BusName")] BusCreateModel bus)
    {
        busService.CreateNewBus(id, bus.BusName);
        return RedirectToAction("Bus");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteBus(int id)
    {
        var bus = busService.findBusById(id);
        if (bus != null)
        {
            busService.deactivateBus(id);
        }
        return RedirectToAction("Bus");
    }


    public IActionResult BusEdit([FromRoute] int id)
    {
        var busEditModel =  new BusEditModel();
        var bus = busService.findBusById(id);
        _logger.LogInformation("Bus" + bus);
        busEditModel = BusEditModel.FromBus(bus);
        return View(busEditModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BusEdit(int id, [Bind("BusName")] BusEditModel bus)
    {
        busService.UpdateBusById(id, bus.BusName);
        return RedirectToAction("Bus");
    }

    [Authorize(Policy = "ManagerRequired")]
    public IActionResult Stop()
    {
        var stops = stopService.getAllStops(); 
        var stopViewModels = stops.Select(stop => new StopViewModel
        {
            Id = stop.Id,
            Name = stop.Name

        }).ToList();
        
        return View(stopViewModels);
    }

    public IActionResult StopEdit([FromRoute] int id)
    {
        var stopEditModel =  new StopEditModel();
        var stop = stopService.findStopById(id);

        stopEditModel = StopEditModel.FromStop(stop);
        return View(stopEditModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StopEdit(int id, [Bind("Name")] StopEditModel stop)
    {
        stopService.UpdateStopById(id, stop.Name);
        return RedirectToAction("Stop");
    }


    public IActionResult StopCreate()
    {
        var stopCreateModel = StopCreateModel.NewStop(stopService.GetLastStopId());
        return View(stopCreateModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StopCreate(int id, [Bind("Name,Latitude,Longitude")] StopCreateModel stop)
    {
        stopService.CreateNewStop(id, stop.Name, stop.Latitude, stop.Longitude);
        return RedirectToAction("Stop");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StopDelete(int id)
    {
        var stop = stopService.findStopById(id);
        if (stop != null)
        {
            stopService.deleteStop(id);
        }
        return RedirectToAction("Stop");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
