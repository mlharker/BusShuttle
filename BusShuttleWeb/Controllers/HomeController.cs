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
    IRouteService routeService;
    ILoopService loopService;

    public HomeController(ILogger<HomeController> logger, IBusService bService, IStopService stService, IRouteService rService, ILoopService lService)
    {
        _logger = logger;
        this.busService = bService;
        this.stopService = stService;
        this.routeService = rService;
        this.loopService = lService;
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

    [Authorize(Policy = "DriverRequired")]
    public IActionResult DriverLoop()
    {
        var loops = loopService.getAllLoops();
        var busses = busService.getAllBusses();

        var viewModel = DriverLoopViewModel.FromData(loops, busses);
        return View(viewModel);
    }

    [Authorize(Policy = "DriverRequired")]
    public IActionResult DriverEntry()
    {
        return View();
    }

    public IActionResult Entry()
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

    [Authorize(Policy = "ManagerRequired")]
    public IActionResult Routes()
    {
        var viewModel = new RouteViewModel
        {
            Loops = loopService.getAllLoops(),
            Routes = new List<Routes>()
        };
        return View(viewModel);
    }


    [Authorize(Policy = "ManagerRequired")]
    [HttpPost]
    public IActionResult Routes(int selectedLoopId)
    {
        var loops = loopService.getActiveLoops();
        var stops = stopService.getActiveStops();
        var selectedLoop = loopService.getLoopById(selectedLoopId);
        var routes = routeService.getRoutesByLoopId(selectedLoopId);
        var viewModel = RouteViewModel.FromLoopID(routes, loops, selectedLoop, stops);
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult CreateRoute(int selectedLoopId, int selectStopId)
    {
        var loops = loopService.getActiveLoops();
        var stops = stopService.getActiveStops();
        var selectedLoop = loopService.getLoopById(selectedLoopId);
        var routes = routeService.getRoutesByLoopId(selectedLoopId);
        var newOrder = routes.Count + 1;
        routeService.CreateNewRoute(newOrder, selectedLoopId, selectStopId);
        return RedirectToAction("Routes");
    }


    [HttpPost]
    public IActionResult MoveRouteUp(int selectedLoopId, int routeId)
    {
        var loops = loopService.getActiveLoops();
        var stops = stopService.getActiveStops();
        var selectedLoop = loopService.getLoopById(selectedLoopId);
        var routes = routeService.getRoutesByLoopId(selectedLoopId);
        routeService.IncreaseRouteOrder(routeId);
        return RedirectToAction("Routes");
    }


    [HttpPost]
    public IActionResult MoveRouteDown(int selectedLoopId, int routeId)
    {
        var loops = loopService.getActiveLoops();
        var stops = stopService.getActiveStops();
        var selectedLoop = loopService.getLoopById(selectedLoopId);
        var routes = routeService.getRoutesByLoopId(selectedLoopId);
        routeService.DecreaseRouteOrder(routeId);
        return RedirectToAction("Routes");
    }

    [HttpPost]
    public IActionResult DeleteRoute(int routeId)
    {
        routeService.DeleteRoute(routeId);
        return RedirectToAction("Routes");
    }

    [Authorize(Policy = "ManagerRequired")]
    public IActionResult Loop()
    {
        var activeLoops = loopService.getActiveLoops();
        var loopViewModels = activeLoops.Select(loop => new LoopViewModel
        {
            Id = loop.Id,
            Name = loop.Name
        }).ToList();

        return View(loopViewModels);
    }

    public IActionResult LoopEdit([FromRoute] int id)
    {
        var loopEditModel = new LoopEditModel();
        var loop = loopService.getLoopById(id);

        loopEditModel = LoopEditModel.FromLoop(loop);
        return View(loopEditModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LoopEdit(int id, [Bind("Name")] LoopEditModel loop)
    {
        loopService.UpdateLoopById(id, loop.Name);
        return RedirectToAction("Loop");
    }

    public IActionResult LoopCreate()
    {
        var loopCreateModel = LoopCreateModel.NewLoop(loopService.GetAmountOfLoops());
        return View(loopCreateModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LoopCreate(int id, [Bind("Name")] LoopCreateModel loop)
    {
        loopService.CreateNewLoop(id, loop.Name);
        return RedirectToAction("Loop");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteLoop(int id)
    {
        var loop = loopService.getLoopById(id); // Retrieve loop from database
        if (loop != null)
        {
            loop.IsActive = false;
            loopService.deactivateLoop(loop);
        }
        return RedirectToAction("Loop");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
