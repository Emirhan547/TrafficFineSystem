using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrafficFineSystem.Data;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Repositories.ApprovalHistoryRepositories;
using TrafficFineSystem.Data.Repositories.TrafficFineRepositories;
using TrafficFineSystem.Data.Repositories.VehicleRepositories;
using TrafficFineSystem.Extensions;
using TrafficFineSystem.Services.AccountServices;
using TrafficFineSystem.Services.ApprovalHistoryServices;
using TrafficFineSystem.Services.TrafficFineServices;
using TrafficFineSystem.Services.VehicleServices;
using TrafficFineSystem.Validators.VehicleValidators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ITrafficFineRepository, TrafficFineRepository>();
builder.Services.AddScoped<ITrafficFineService, TrafficFineService>();
builder.Services.AddScoped<IApprovalHistoryRepository, ApprovalHistoryRepository>();
builder.Services.AddScoped<IApprovalHistoryService, ApprovalHistoryService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddControllersWithViews();
builder.Services.AddValidatorsFromAssemblyContaining<CreateVehicleValidator>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.SeedRolesAsync();
    await scope.ServiceProvider.SeedUsersAsync();
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
