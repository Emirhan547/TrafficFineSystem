using Microsoft.AspNetCore.Mvc;
using TrafficFineSystem.Dtos.VehicleDtos;
using TrafficFineSystem.Services.VehicleServices;

namespace TrafficFineSystem.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return View(vehicles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _vehicleService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _vehicleService.GetForUpdateAsync(id);

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateVehicleDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _vehicleService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);    

            return View(vehicle);
        }
    }
}
