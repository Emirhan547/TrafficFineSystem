using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.GenericRepositories;

namespace TrafficFineSystem.Data.Repositories.TrafficFineRepositories
{
    public interface ITrafficFineRepository : IGenericRepository<TrafficFine>
    {
        Task<List<TrafficFine>> GetAllWithVehiclesAsync();
        Task<TrafficFine?> GetByIdWithVehicleAsync(int id);
    }
}
