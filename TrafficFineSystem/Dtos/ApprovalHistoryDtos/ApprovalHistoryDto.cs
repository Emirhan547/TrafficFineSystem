namespace TrafficFineSystem.Dtos.ApprovalHistoryDtos
{
    public class ApprovalHistoryDto
    {
        public string UserEmail { get; set; } = null!;

        public string Action { get; set; } = null!;

        public string PreviousStatus { get; set; } = null!;

        public string NewStatus { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
