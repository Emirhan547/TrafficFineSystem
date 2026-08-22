using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrafficFineSystem.Data.Entities;

namespace TrafficFineSystem.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<TrafficFine> TrafficFines { get; set; }

        public DbSet<ApprovalHistory> ApprovalHistories { get; set; }
    }
}