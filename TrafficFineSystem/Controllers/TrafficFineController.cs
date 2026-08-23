using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrafficFineSystem.Dtos.TrafficFineDtos;
using TrafficFineSystem.Services.TrafficFineServices;

namespace TrafficFineSystem.Controllers
{
    [Authorize]
    public class TrafficFineController : Controller
    {
        private readonly ITrafficFineService _trafficFineService;

        public TrafficFineController(
            ITrafficFineService trafficFineService)
        {
            _trafficFineService = trafficFineService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trafficFines = await _trafficFineService.GetAllGroupedAsync();
            return View(trafficFines);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Vehicles =await _trafficFineService.GetVehiclesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrafficFineDto dto)
        {
            await _trafficFineService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var trafficFine =await _trafficFineService.GetForUpdateAsync(id);
            ViewBag.Vehicles =await _trafficFineService.GetVehiclesAsync();
            return View(trafficFine);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTrafficFineDto dto)
        {
            await _trafficFineService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var trafficFine = await _trafficFineService.GetByIdAsync(id);
            return View(trafficFine);
        }
    }
}