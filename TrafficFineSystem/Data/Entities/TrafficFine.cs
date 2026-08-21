using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Data.Entities
{
    public class TrafficFine
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public decimal Amount { get; set; }

        public DateTime FineDate { get; set; }

        public string Description { get; set; } 

        public FineStatus Status { get; set; }

        public Vehicle Vehicle { get; set; } 

        public IList<ApprovalHistory> ApprovalHistories { get; set; }

    }
}
