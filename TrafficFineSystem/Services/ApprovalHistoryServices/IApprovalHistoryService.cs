using TrafficFineSystem.Dtos.ApprovalHistoryDtos;
using TrafficFineSystem.Dtos.TrafficFineDtos;

namespace TrafficFineSystem.Services.ApprovalHistoryServices
{
    public interface IApprovalHistoryService
    {
        Task<List<ApprovalHistoryDto>> GetHistoryAsync(int trafficFineId);
        Task<bool> ApproveAsync(ApprovalDto dto,int userId);
        Task<bool> RejectAsync(ApprovalDto dto,int userId);
        Task<List<TrafficFineListDto>> GetAllTrafficFinesAsync();
    }
}
