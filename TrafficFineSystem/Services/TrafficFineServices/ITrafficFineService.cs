using TrafficFineSystem.Dtos.TrafficFineDtos;
using TrafficFineSystem.Dtos.VehicleDtos;

namespace TrafficFineSystem.Services.TrafficFineServices
{
    public interface ITrafficFineService
    {
        Task<List<TrafficFineListDto>> GetAllAsync();
        Task<TrafficFineDetailDto?> GetByIdAsync(int id);
        Task<UpdateTrafficFineDto?> GetForUpdateAsync(int id);
        Task<List<VehicleListDto>> GetVehiclesAsync();
        Task CreateAsync(CreateTrafficFineDto dto);
        Task <bool>UpdateAsync(UpdateTrafficFineDto dto);
        Task<List<VehicleTrafficFineDto>> GetAllGroupedAsync();
    }
}
