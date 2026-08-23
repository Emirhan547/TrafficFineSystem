using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Enums;
using TrafficFineSystem.Data.Repositories.ApprovalHistoryRepositories;
using TrafficFineSystem.Data.Repositories.TrafficFineRepositories;
using TrafficFineSystem.Dtos.ApprovalHistoryDtos;
using TrafficFineSystem.Dtos.TrafficFineDtos;
using TrafficFineSystem.Services.Common;

namespace TrafficFineSystem.Services.ApprovalHistoryServices
{
    public class ApprovalService : IApprovalService
    {
        private readonly IApprovalRepository _approvalHistoryRepository;
        private readonly ITrafficFineRepository _trafficFineRepository;

        public ApprovalService(ITrafficFineRepository trafficFineRepository, IApprovalRepository approvalHistoryRepository)
        {
            _trafficFineRepository = trafficFineRepository;
            _approvalHistoryRepository = approvalHistoryRepository;
        }

        public async Task<ServiceResult> ApproveAsync(ApproveTrafficFineDto dto,int userId,string role)
        {
            var trafficFine =await _trafficFineRepository.GetByIdAsync(dto.TrafficFineId);

            if (trafficFine is null)
            {
                return ServiceResult.Failure("Trafik cezası bulunamadı.");
            }

            var previousStatus = trafficFine.Status;

            if (role == "Manager")
            {
                if (trafficFine.Status != FineStatus.New)
                {
                    return ServiceResult.Failure("Bu ceza mevcut aşamanızda onaylanamaz.");
                }
                trafficFine.Status = FineStatus.ManagerApproved;
            }
            else if (role == "Finance")
            {
                if (trafficFine.Status != FineStatus.ManagerApproved)
                {
                    return ServiceResult.Failure("Bu ceza mevcut aşamanızda onaylanamaz.");
                }

                trafficFine.Status =FineStatus.Completed;
            }
            else
            {
                return ServiceResult.Failure("Bu işlem için yetkiniz bulunmamaktadır.");
            }
            var history = new ApprovalHistory
            {
                TrafficFineId = trafficFine.Id,
                UserId = userId,
                Action = ApprovalAction.Approved,
                PreviousStatus = previousStatus.ToString(),
                NewStatus = trafficFine.Status.ToString(),
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };
            await _approvalHistoryRepository.AddAsync(history);
            await _trafficFineRepository.Update(trafficFine);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RejectAsync(RejectTrafficFineDto dto,int userId,string role)
        {
            var trafficFine =await _trafficFineRepository.GetByIdAsync( dto.TrafficFineId);
            if (trafficFine is null)
            {
                return ServiceResult.Failure("Trafik cezası bulunamadı.");
            }

            if (role == "Manager")
            {
                if (trafficFine.Status != FineStatus.New)
                {
                    return ServiceResult.Failure("Bu ceza mevcut aşamanızda reddedilemez.");
                }
            }
            else if (role == "Finance")
            {
                if (trafficFine.Status != FineStatus.ManagerApproved)
                {
                    return ServiceResult.Failure("Bu ceza mevcut aşamanızda reddedilemez.");
                }
            }
            else
            {
                return ServiceResult.Failure( "Bu işlem için yetkiniz bulunmamaktadır.");
            }

            var previousStatus = trafficFine.Status;
            trafficFine.Status = FineStatus.Rejected;
            var history = new ApprovalHistory
            {
                TrafficFineId = trafficFine.Id,
                UserId = userId,
                Action = ApprovalAction.Rejected,
                PreviousStatus = previousStatus.ToString(),
                NewStatus = trafficFine.Status.ToString(),
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };

            await _approvalHistoryRepository.AddAsync(history);
            await _trafficFineRepository.Update(trafficFine);
            return ServiceResult.Success();
        }

        public async Task<List<ApprovalHistoryDto>> GetHistoryAsync(int trafficFineId)
        {
            var histories = await _approvalHistoryRepository.GetByTrafficFineIdAsync(trafficFineId);
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

        public async Task<List<TrafficFineListDto>>GetAllTrafficFinesAsync()
        {
            var trafficFines =await _trafficFineRepository.GetAllWithVehiclesAsync();
            return trafficFines.Select(x => new TrafficFineListDto
            {
                Id = x.Id,
                Plate = x.Vehicle.Plate,
                Amount = x.Amount,
                FineDate = x.FineDate,
                Status = x.Status
            }).ToList();
        }

        public async Task<List<VehicleTrafficFineDto>>GetAllGroupedAsync()
        {
            var trafficFines =await _trafficFineRepository.GetAllWithVehiclesAsync();
            return trafficFines.GroupBy(x => x.VehicleId).Select(group => new VehicleTrafficFineDto
                {
                    VehicleId = group.Key,
                    Plate = group.First().Vehicle.Plate,
                    Brand = group.First().Vehicle.Brand,
                    Model = group.First().Vehicle.Model,

                    TrafficFines = group.Select(x =>
                        new TrafficFineListDto
                        {
                            Id = x.Id,
                            Plate = x.Vehicle.Plate,
                            Amount = x.Amount,
                            FineDate = x.FineDate,
                            Status = x.Status
                        }).ToList()
                }).ToList();
        }
    }
}