namespace TrafficFineSystem.Dtos.TrafficFineDtos
{
    public class VehicleTrafficFineDto
    {
        public int VehicleId { get; set; }

        public string Plate { get; set; } 

        public string Brand { get; set; } 

        public string Model { get; set; }

        public List<TrafficFineListDto> TrafficFines { get; set; }
    }
}
