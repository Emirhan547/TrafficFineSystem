using TrafficFineSystem.Data;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.VehicleRepositories;
using TrafficFineSystem.Dtos.VehicleDtos;

namespace TrafficFineSystem.Services.VehicleServices
{
    public class VehicleService : IVehicleService
    {
        private readonly AppDbContext _context;
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(AppDbContext context, IVehicleRepository vehicleRepository)
        {
            _context = context;
            _vehicleRepository = vehicleRepository;
        }

        public async Task CreateAsync(CreateVehicleDto dto)
        {
            var plateExists =
                await _vehicleRepository.PlateExistsAsync(dto.Plate);

            if (plateExists)
                throw new InvalidOperationException(
                    "Bu plakaya sahip bir araç zaten mevcut.");

            var vehicle = new Vehicle
            {
                Plate = dto.Plate,
                VehicleType = dto.VehicleType,
                Brand = dto.Brand,
                Model = dto.Model
            };

            await _vehicleRepository.AddAsync(vehicle);

            await _context.SaveChangesAsync();
        }

        public async Task<List<VehicleListDto>> GetAllAsync()
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

        public async Task<VehicleListDto?> GetByIdAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle is null)
                return null;

            return new VehicleListDto
            {
                Id = vehicle.Id,
                Plate = vehicle.Plate,
                VehicleType = vehicle.VehicleType,
                Brand = vehicle.Brand,
                Model = vehicle.Model
            };
        }

        public async Task<UpdateVehicleDto?> GetForUpdateAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle is null)
                return null;

            return new UpdateVehicleDto
            {
                Id = vehicle.Id,
                Plate = vehicle.Plate,
                VehicleType = vehicle.VehicleType,
                Brand = vehicle.Brand,
                Model = vehicle.Model
            };
        }

        public async Task<bool> UpdateAsync(UpdateVehicleDto dto)
        {
            var vehicle =
                await _vehicleRepository.GetByIdAsync(dto.Id);

            if (vehicle is null)
                return false;

            var plateExists =
                await _vehicleRepository.PlateExistsAsync(
                    dto.Plate,
                    dto.Id);

            if (plateExists)
                throw new InvalidOperationException(
                    "Bu plakaya sahip başka bir araç zaten mevcut.");

            vehicle.Plate = dto.Plate;
            vehicle.VehicleType = dto.VehicleType;
            vehicle.Brand = dto.Brand;
            vehicle.Model = dto.Model;

            _vehicleRepository.Update(vehicle);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
