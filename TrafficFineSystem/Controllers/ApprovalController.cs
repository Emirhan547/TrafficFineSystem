using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrafficFineSystem.Dtos.ApprovalHistoryDtos;
using TrafficFineSystem.Services.ApprovalHistoryServices;
using TrafficFineSystem.Services.TrafficFineServices;

namespace TrafficFineSystem.Controllers
{
    public class ApprovalController : Controller
    {
        private readonly IApprovalHistoryService _approvalService;
        private readonly ITrafficFineService _trafficFineService;
        public ApprovalController(
            IApprovalHistoryService approvalService, ITrafficFineService trafficFineService)
        {
            _approvalService = approvalService;
            _trafficFineService = trafficFineService;
        }
        [HttpGet]
        [Authorize(Roles = "Manager,Finance")]
        public async Task<IActionResult> Index()
        {
            var trafficFines =
                await _approvalService.GetAllTrafficFinesAsync();

            return View(trafficFines);
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Finance")]
        public async Task<IActionResult> History(int trafficFineId)
        {
            var histories =
                await _approvalService.GetHistoryAsync(trafficFineId);

            return View(histories);
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Finance")]
        public async Task<IActionResult> Approve(ApprovalDto dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(
                    "Details",
                    "TrafficFine",
                    new { id = dto.TrafficFineId });

            var userId =
                int.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result =
                await _approvalService.ApproveAsync(
                    dto,
                    userId);

            if (!result)
                return NotFound();

            TempData["Success"] =
                "Onay işlemi başarıyla gerçekleştirildi.";

            return RedirectToAction(
                "Details",
                "TrafficFine",
                new { id = dto.TrafficFineId });
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Finance")]
        public async Task<IActionResult> Reject(ApprovalDto dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(
                    "Details",
                    "TrafficFine",
                    new { id = dto.TrafficFineId });

            var userId =
                int.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result =
                await _approvalService.RejectAsync(
                    dto,
                    userId);

            if (!result)
                return NotFound();

            TempData["Success"] =
                "Ceza reddedildi.";

            return RedirectToAction(
                "Details",
                "TrafficFine",
                new { id = dto.TrafficFineId });
        }
    }
}