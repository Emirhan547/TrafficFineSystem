namespace TrafficFineSystem.Dtos.TrafficFineDtos
{
    public class UpdateTrafficFineDto
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public decimal Amount { get; set; }

        public DateTime FineDate { get; set; }

        public string Description { get; set; } 
    }
}
