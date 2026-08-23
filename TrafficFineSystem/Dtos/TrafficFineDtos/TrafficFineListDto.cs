using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Dtos.TrafficFineDtos
{
    public class TrafficFineListDto
    {
        public int Id { get; set; }

        public string Plate { get; set; } 

        public decimal Amount { get; set; }

        public DateTime FineDate { get; set; }

        public FineStatus Status { get; set; }
    }
}
