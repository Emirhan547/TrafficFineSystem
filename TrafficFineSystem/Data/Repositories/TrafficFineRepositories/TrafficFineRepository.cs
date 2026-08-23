using Microsoft.EntityFrameworkCore;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.GenericRepositories;

namespace TrafficFineSystem.Data.Repositories.TrafficFineRepositories
{
    public class TrafficFineRepository : GenericRepository<TrafficFine>, ITrafficFineRepository
    {
        public TrafficFineRepository(AppDbContext context)
            : base(context)
        {
        }
        public async Task<List<TrafficFine>> GetAllWithVehiclesAsync()
        {
            return await _dbSet
                .Include(x => x.Vehicle)
                .ToListAsync();
        }

        public async Task<TrafficFine?> GetByIdWithVehicleAsync(int id)
        {
            return await _dbSet
                .Include(x => x.Vehicle)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}