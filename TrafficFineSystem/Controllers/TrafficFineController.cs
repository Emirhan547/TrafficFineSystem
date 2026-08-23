using Microsoft.AspNetCore.Mvc;
using TrafficFineSystem.Dtos.TrafficFineDtos;
using TrafficFineSystem.Services.TrafficFineServices;

namespace TrafficFineSystem.Controllers
{
    public class TrafficFineController : Controller
    {
        private readonly ITrafficFineService _trafficFineService;

        public TrafficFineController(ITrafficFineService trafficFineService)
        {
            _trafficFineService = trafficFineService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trafficFines = await _trafficFineService.GetAllAsync();

            return View(trafficFines);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vehicles = await _trafficFineService.GetVehiclesAsync();

            ViewBag.Vehicles = vehicles;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
      CreateTrafficFineDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Vehicles =
                    await _trafficFineService.GetVehiclesAsync();

                return View(dto);
            }

            await _trafficFineService.CreateAsync(dto);

            TempData["Success"] =
                "Trafik cezası başarıyla oluşturuldu.";

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var trafficFine =
                await _trafficFineService.GetForUpdateAsync(id);

            if (trafficFine is null)
                return NotFound();

            ViewBag.Vehicles =
                await _trafficFineService.GetVehiclesAsync();

            return View(trafficFine);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
    UpdateTrafficFineDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Vehicles =
                    await _trafficFineService.GetVehiclesAsync();

                return View(dto);
            }

            var result =
                await _trafficFineService.UpdateAsync(dto);

            if (!result)
                return NotFound();

            TempData["Success"] =
                "Trafik cezası başarıyla güncellendi.";

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var trafficFine =
                await _trafficFineService.GetByIdAsync(id);

            if (trafficFine is null)
                return NotFound();

            return View(trafficFine);
        }
    }
}
