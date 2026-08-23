using TrafficFineSystem.Extensions;
using TrafficFineSystem.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProjectServices(builder.Configuration);
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.SeedRolesAsync();
    await scope.ServiceProvider.SeedUsersAsync();
    await scope.ServiceProvider.SeedDataAsync();
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
    pattern: "{controller=Vehicle}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
