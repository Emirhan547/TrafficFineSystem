using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Dtos.TrafficFineDtos
{
    public class TrafficFineDetailDto
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public string Plate { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime FineDate { get; set; }

        public string Description { get; set; } = null!;

        public FineStatus Status { get; set; }
    }
}
