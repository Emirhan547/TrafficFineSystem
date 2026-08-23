using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrafficFineSystem.Data;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.ApprovalHistoryRepositories;
using TrafficFineSystem.Data.Repositories.TrafficFineRepositories;
using TrafficFineSystem.Data.Repositories.VehicleRepositories;
using TrafficFineSystem.Services.AccountServices;
using TrafficFineSystem.Services.ApprovalHistoryServices;
using TrafficFineSystem.Services.TrafficFineServices;
using TrafficFineSystem.Services.VehicleServices;
using TrafficFineSystem.Validators.VehicleValidators;

namespace TrafficFineSystem.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<AppUser, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleService, VehicleService>();

            services.AddScoped<ITrafficFineRepository, TrafficFineRepository>();
            services.AddScoped<ITrafficFineService, TrafficFineService>();

            services.AddScoped<IApprovalRepository, ApprovalRepository>();
            services.AddScoped<IApprovalService, ApprovalService>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddValidatorsFromAssemblyContaining<CreateVehicleValidator>();

            return services;
        }
    }
}
