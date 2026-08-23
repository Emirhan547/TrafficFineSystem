using TrafficFineSystem.Dtos.VehicleDtos;
using TrafficFineSystem.Services.Common;

namespace TrafficFineSystem.Services.VehicleServices
{
    public interface IVehicleService
    {
        Task<List<VehicleListDto>> GetAllAsync();

        Task<VehicleListDto?> GetByIdAsync(int id);

        Task<UpdateVehicleDto?> GetForUpdateAsync(int id);

        Task<ServiceResult> CreateAsync(CreateVehicleDto dto);

        Task<ServiceResult> UpdateAsync(UpdateVehicleDto dto);
    }
}
