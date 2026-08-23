using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Dtos.ApprovalHistoryDtos
{
    public class ApprovalHistoryDto
    {
        public string UserEmail { get; set; } 

        public ApprovalAction Action { get; set; }

        public string PreviousStatus { get; set; } 

        public string NewStatus { get; set; } 

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
