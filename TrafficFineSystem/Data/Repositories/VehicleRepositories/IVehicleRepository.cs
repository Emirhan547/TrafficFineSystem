using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.GenericRepositories;

namespace TrafficFineSystem.Data.Repositories.VehicleRepositories
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        Task<bool> PlateExistsAsync(string plate);

        Task<bool> PlateExistsAsync(string plate, int excludedVehicleId);
    }
}
