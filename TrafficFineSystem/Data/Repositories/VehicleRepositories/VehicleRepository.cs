using Microsoft.EntityFrameworkCore;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.GenericRepositories;

namespace TrafficFineSystem.Data.Repositories.VehicleRepositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<bool> PlateExistsAsync(string plate)
        {
            return await _dbSet .AnyAsync(x => x.Plate == plate);
        }

        public async Task<bool> PlateExistsAsync(string plate,int excludedVehicleId)
        {
            return await _dbSet.AnyAsync(x => x.Plate == plate &&x.Id != excludedVehicleId);
        }
    }
}
