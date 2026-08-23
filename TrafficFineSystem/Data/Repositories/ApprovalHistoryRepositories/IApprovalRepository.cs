using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.GenericRepositories;

namespace TrafficFineSystem.Data.Repositories.ApprovalHistoryRepositories
{
    public interface IApprovalRepository : IGenericRepository<ApprovalHistory>
    {
        Task<List<ApprovalHistory>> GetByTrafficFineIdAsync(int trafficFineId);
    }
}
