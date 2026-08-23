using Microsoft.AspNetCore.Identity;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Dtos.AccountDtos;

namespace TrafficFineSystem.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> LoginAsync(LoginDto dto)
        {
            var user =await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
                return false;

            var result =await _signInManager.PasswordSignInAsync(user,dto.Password,isPersistent: false,lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
