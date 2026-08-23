using TrafficFineSystem.Dtos.ApprovalHistoryDtos;
using TrafficFineSystem.Dtos.TrafficFineDtos;
using TrafficFineSystem.Services.Common;

namespace TrafficFineSystem.Services.ApprovalHistoryServices
{
    public interface IApprovalService
    {
        Task<List<TrafficFineListDto>> GetAllTrafficFinesAsync();
        Task<List<ApprovalHistoryDto>> GetHistoryAsync(int trafficFineId);
        Task<ServiceResult> ApproveAsync(ApproveTrafficFineDto dto,int userId,string role);
        Task<ServiceResult> RejectAsync(RejectTrafficFineDto dto,int userId,string role);
        Task<List<VehicleTrafficFineDto>> GetAllGroupedAsync();
    }
}
