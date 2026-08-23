using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Dtos.VehicleDtos
{
    public class CreateVehicleDto
    {
        public string Plate { get; set; } 

        public VehicleType VehicleType { get; set; }

        public string Brand { get; set; } 

        public string Model { get; set; }
    }
}
