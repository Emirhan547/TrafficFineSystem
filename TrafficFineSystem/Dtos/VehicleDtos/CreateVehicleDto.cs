using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Dtos.VehicleDtos
{
    public class CreateVehicleDto
    {
        public string Plate { get; set; } = null!;

        public VehicleType VehicleType { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;
    }
}
