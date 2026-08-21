using Microsoft.AspNetCore.Identity;

namespace TrafficFineSystem.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public IList<ApprovalHistory> ApprovalHistories { get; set; }
    }
}
