using TrafficFineSystem.Dtos.VehicleDtos;

namespace TrafficFineSystem.Services.VehicleServices
{
    public interface IVehicleService
    {
        Task<List<VehicleListDto>> GetAllAsync();

        Task<VehicleListDto?> GetByIdAsync(int id);

        Task<UpdateVehicleDto?> GetForUpdateAsync(int id);

        Task CreateAsync(CreateVehicleDto dto);

        Task<bool> UpdateAsync(UpdateVehicleDto dto);
    }
}
