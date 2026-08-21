namespace TrafficFineSystem.Data.Entities
{
    public class ApprovalHistory
    {
        public int Id { get; set; }

        public int TrafficFineId { get; set; }

        public int UserId { get; set; }

        public string Action { get; set; } = null!;

        public string PreviousStatus { get; set; }

        public string NewStatus { get; set; } 

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public TrafficFine TrafficFine { get; set; }

        public AppUser User { get; set; } 
    }
}
