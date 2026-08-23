using TrafficFineSystem.Dtos.ApprovalHistoryDtos;
using TrafficFineSystem.Dtos.TrafficFineDtos;

namespace TrafficFineSystem.Services.ApprovalHistoryServices
{
    public interface IApprovalService
    {
        Task<List<TrafficFineListDto>> GetAllTrafficFinesAsync();
        Task<List<ApprovalHistoryDto>> GetHistoryAsync(int trafficFineId);
        Task<bool> ApproveAsync(ApproveTrafficFineDto dto,int userId,string role);
        Task<bool> RejectAsync(RejectTrafficFineDto dto,int userId,string role);
        Task<List<VehicleTrafficFineDto>> GetAllGroupedAsync();
    }
}
