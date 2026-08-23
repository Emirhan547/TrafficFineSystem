using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrafficFineSystem.Dtos.ApprovalHistoryDtos;
using TrafficFineSystem.Services.ApprovalHistoryServices;

namespace TrafficFineSystem.Controllers
{
    [Authorize(Roles = "Manager,Finance")]
    public class ApprovalController : Controller
    {
        private readonly IApprovalService _approvalService;

        public ApprovalController(IApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trafficFines =await _approvalService.GetAllGroupedAsync();
            return View(trafficFines);
        }

        [HttpGet]
        public async Task<IActionResult> History(int trafficFineId)
        {
            var histories =await _approvalService.GetHistoryAsync(trafficFineId);
            return View(histories);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(ApproveTrafficFineDto dto)
        {
            var userId =int.Parse( User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role =User.IsInRole("Manager")? "Manager": "Finance";
            var result =await _approvalService.ApproveAsync( dto,userId,role);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.ErrorMessage;

                return RedirectToAction("Details","TrafficFine",
                    new { id = dto.TrafficFineId });
            }

            return RedirectToAction("Details","TrafficFine",
                new { id = dto.TrafficFineId });
        }

        [HttpPost]
        public async Task<IActionResult> Reject(RejectTrafficFineDto dto)
        {
            var userId =int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role =User.IsInRole("Manager")? "Manager": "Finance";

            var result =await _approvalService.RejectAsync(dto,userId,role);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.ErrorMessage;

                return RedirectToAction("Details","TrafficFine",
                    new { id = dto.TrafficFineId });
            }

            return RedirectToAction( "Details","TrafficFine",
                new { id = dto.TrafficFineId });
        }
    }
}