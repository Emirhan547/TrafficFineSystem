using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Dtos.TrafficFineDtos
{
    public class TrafficFineDetailDto
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public string Plate { get; set; } 

        public string Brand { get; set; } 

        public string Model { get; set; } 

        public decimal Amount { get; set; }

        public DateTime FineDate { get; set; }

        public string Description { get; set; } 

        public FineStatus Status { get; set; }
    }
}
