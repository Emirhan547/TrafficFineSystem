using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Enums;
using TrafficFineSystem.Data.Repositories.TrafficFineRepositories;
using TrafficFineSystem.Data.Repositories.VehicleRepositories;
using TrafficFineSystem.Dtos.TrafficFineDtos;
using TrafficFineSystem.Dtos.VehicleDtos;


namespace TrafficFineSystem.Services.TrafficFineServices
{
    public class TrafficFineService : ITrafficFineService
    {
        private readonly ITrafficFineRepository _trafficFineRepository;
        private readonly IVehicleRepository _vehicleRepository;


        public TrafficFineService(ITrafficFineRepository trafficFineRepository, IVehicleRepository vehicleRepository)
        {
            _trafficFineRepository = trafficFineRepository;
            _vehicleRepository = vehicleRepository;

        }
        public async Task CreateAsync(CreateTrafficFineDto dto)
        {
            var trafficFine = new TrafficFine
            {
                VehicleId = dto.VehicleId,
                Amount = dto.Amount,
                FineDate = dto.FineDate,
                Description = dto.Description,
                Status = FineStatus.New
            };

            await _trafficFineRepository.AddAsync(trafficFine);

        }

        public async Task<List<TrafficFineListDto>> GetAllAsync()
        {
            var trafficFines = await _trafficFineRepository.GetAllWithVehiclesAsync();
            return trafficFines.Select(x => new TrafficFineListDto
            {
                Id = x.Id,
                Plate = x.Vehicle.Plate,
                Amount = x.Amount,
                FineDate = x.FineDate,
                Status = x.Status
            }).ToList();
        }

        public async Task<TrafficFineDetailDto?> GetByIdAsync(int id)
        {
            var trafficFine = await _trafficFineRepository.GetByIdWithVehicleAsync(id);
            return new TrafficFineDetailDto
            {
                Id = trafficFine.Id,
                VehicleId = trafficFine.VehicleId,
                Plate = trafficFine.Vehicle.Plate,
                Brand = trafficFine.Vehicle.Brand,
                Model = trafficFine.Vehicle.Model,
                Amount = trafficFine.Amount,
                FineDate = trafficFine.FineDate,
                Description = trafficFine.Description,
                Status = trafficFine.Status
            };
        }

        public async Task<UpdateTrafficFineDto?> GetForUpdateAsync(int id)
        {
            var trafficFine = await _trafficFineRepository.GetByIdAsync(id);

            if (trafficFine.Status != FineStatus.New)
                return null;

            return new UpdateTrafficFineDto
            {
                Id = trafficFine.Id,
                VehicleId = trafficFine.VehicleId,
                Amount = trafficFine.Amount,
                FineDate = trafficFine.FineDate,
                Description = trafficFine.Description
            };
        }

        public async Task<List<VehicleListDto>> GetVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            return vehicles.Select(vehicle => new VehicleListDto
            {
                Id = vehicle.Id,
                Plate = vehicle.Plate,
                VehicleType = vehicle.VehicleType,
                Brand = vehicle.Brand,
                Model = vehicle.Model
            }).ToList();
        }

        public async Task<bool> UpdateAsync(UpdateTrafficFineDto dto)
        {
            var trafficFine = await _trafficFineRepository.GetByIdAsync(dto.Id);

            if (trafficFine.Status != FineStatus.New)
                return false;

            trafficFine.VehicleId = dto.VehicleId;
            trafficFine.Amount = dto.Amount;
            trafficFine.FineDate = dto.FineDate;
            trafficFine.Description = dto.Description;

            _trafficFineRepository.Update(trafficFine);


            return true;
        }
        public async Task<List<VehicleTrafficFineDto>> GetAllGroupedAsync()
        {
            var trafficFines = await _trafficFineRepository.GetAllWithVehiclesAsync();

            return trafficFines.GroupBy(x => x.VehicleId)
                .Select(group => new VehicleTrafficFineDto
                {
                    VehicleId = group.Key,
                    Plate = group.First().Vehicle.Plate,
                    Brand = group.First().Vehicle.Brand,
                    Model = group.First().Vehicle.Model,

                    TrafficFines = group.Select(x => new TrafficFineListDto
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

