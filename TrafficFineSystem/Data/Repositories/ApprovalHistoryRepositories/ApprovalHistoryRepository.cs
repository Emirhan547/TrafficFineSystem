using Microsoft.EntityFrameworkCore;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.GenericRepositories;

namespace TrafficFineSystem.Data.Repositories.ApprovalHistoryRepositories
{
    public class ApprovalHistoryRepository : GenericRepository<ApprovalHistory>,IApprovalHistoryRepository
    {
        public ApprovalHistoryRepository(AppDbContext context): base(context)
        {
        }
        public async Task<List<ApprovalHistory>> GetByTrafficFineIdAsync(int trafficFineId)
        {
            return await _dbSet.Include(x => x.User).Where(x => x.TrafficFineId == trafficFineId).OrderByDescending(x => x.CreatedAt) .ToListAsync();
        }
    }
}
