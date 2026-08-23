using TrafficFineSystem.Data;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Enums;
using TrafficFineSystem.Data.Repositories.ApprovalHistoryRepositories;
using TrafficFineSystem.Data.Repositories.TrafficFineRepositories;
using TrafficFineSystem.Dtos.ApprovalHistoryDtos;
using TrafficFineSystem.Dtos.TrafficFineDtos;

namespace TrafficFineSystem.Services.ApprovalHistoryServices
{
    public class ApprovalHistoryService : IApprovalHistoryService
    {
        private readonly IApprovalHistoryRepository _approvalHistoryRepository;
        private readonly ITrafficFineRepository _trafficFineRepository;

        public ApprovalHistoryService(ITrafficFineRepository trafficFineRepository,IApprovalHistoryRepository approvalHistoryRepository)
        {
            _trafficFineRepository = trafficFineRepository;
           
            _approvalHistoryRepository = approvalHistoryRepository;
        }

        public async Task<bool> ApproveAsync(ApprovalDto dto, int userId)
        {
            var trafficFine =await _trafficFineRepository.GetByIdAsync(dto.TrafficFineId);

            if (trafficFine is null)
                return false;

            var previousStatus = trafficFine.Status;
            if (trafficFine.Status == FineStatus.New)
            {
                trafficFine.Status =FineStatus.ManagerApproved;
            }
            else if (trafficFine.Status == FineStatus.ManagerApproved)
            {
                trafficFine.Status =FineStatus.Completed;
            }
            else
            {
                return false;
            }

            var history = new ApprovalHistory
            {
                TrafficFineId = trafficFine.Id,
                UserId = userId,
                Action = "Approve",
                PreviousStatus = previousStatus.ToString(),
                NewStatus = trafficFine.Status.ToString(),
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };

            await _approvalHistoryRepository.AddAsync(history);

            _trafficFineRepository.Update(trafficFine);

           

            return true;
        }

        public async Task<List<ApprovalHistoryDto>> GetHistoryAsync(int trafficFineId)
        {
            var histories =await _approvalHistoryRepository.GetByTrafficFineIdAsync(trafficFineId);
            return histories.Select(x => new ApprovalHistoryDto
            {
                UserEmail = x.User.Email!,
                Action = x.Action,
                PreviousStatus = x.PreviousStatus,
                NewStatus = x.NewStatus,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            }).ToList();
        }

        public async Task<bool> RejectAsync(ApprovalDto dto,int userId)
        {
            var trafficFine =await _trafficFineRepository.GetByIdAsync(dto.TrafficFineId);

            if (trafficFine is null)
                return false;

            if (trafficFine.Status != FineStatus.New &&
                trafficFine.Status != FineStatus.ManagerApproved)
            {
                return false;
            }

            var previousStatus = trafficFine.Status;
            trafficFine.Status = FineStatus.Rejected;
            var history = new ApprovalHistory
            {
                TrafficFineId = trafficFine.Id,
                UserId = userId,
                Action = "Reject",
                PreviousStatus = previousStatus.ToString(),
                NewStatus = trafficFine.Status.ToString(),
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };
            await _approvalHistoryRepository.AddAsync(history);
            _trafficFineRepository.Update(trafficFine);
            return true;
        }
        public async Task<List<TrafficFineListDto>> GetAllTrafficFinesAsync()
        {
            var trafficFines =
                await _trafficFineRepository.GetAllWithVehiclesAsync();

            return trafficFines.Select(x => new TrafficFineListDto
            {
                Id = x.Id,
                Plate = x.Vehicle.Plate,
                Amount = x.Amount,
                FineDate = x.FineDate,
                Status = x.Status
            }).ToList();
        }
    }
}
