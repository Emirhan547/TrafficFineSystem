using Microsoft.AspNetCore.Mvc;
using TrafficFineSystem.Dtos.AccountDtos;
using TrafficFineSystem.Services.AccountServices;

namespace TrafficFineSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(
            IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
           
            var result =await _accountService.LoginAsync(dto);
            return RedirectToAction( "Index","Vehicle");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
