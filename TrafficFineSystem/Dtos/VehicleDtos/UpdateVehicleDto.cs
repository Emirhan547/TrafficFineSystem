using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Dtos.VehicleDtos
{
    public class UpdateVehicleDto
    {
        public int Id { get; set; }

        public string Plate { get; set; } 

        public VehicleType VehicleType { get; set; }

        public string Brand { get; set; } 

        public string Model { get; set; } 
    }
}
