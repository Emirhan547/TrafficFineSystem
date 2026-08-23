using Microsoft.EntityFrameworkCore;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.GenericRepositories;

namespace TrafficFineSystem.Data.Repositories.ApprovalHistoryRepositories
{
    public class ApprovalRepository : GenericRepository<ApprovalHistory>, IApprovalRepository
    {
        public ApprovalRepository(AppDbContext context): base(context)
        {
        }
        public async Task<List<ApprovalHistory>> GetByTrafficFineIdAsync(int trafficFineId)
        {
            return await _dbSet.Include(x => x.User).Where(x => x.TrafficFineId == trafficFineId).OrderByDescending(x => x.CreatedAt) .ToListAsync();
        }
    }
}
