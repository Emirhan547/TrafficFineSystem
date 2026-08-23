using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.GenericRepositories;

namespace TrafficFineSystem.Data.Repositories.ApprovalHistoryRepositories
{
    public interface IApprovalHistoryRepository: IGenericRepository<ApprovalHistory>
    {
        Task<List<ApprovalHistory>> GetByTrafficFineIdAsync(int trafficFineId);
    }
}
