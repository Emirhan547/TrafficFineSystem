using TrafficFineSystem.Dtos.AccountDtos;

namespace TrafficFineSystem.Services.AccountServices
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(LoginDto dto);

        Task LogoutAsync();
    }
}
