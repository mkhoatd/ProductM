using Microsoft.EntityFrameworkCore;
using ProductM.Models;
using ProductM.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
string connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
services.AddControllersWithViews();
services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

services.AddTransient<IProductService, ProductService>();

WebApplication app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services1 = scope.ServiceProvider;
    try
    {
        var context=services1.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception e)
    {
        var logger = services1.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred while migrating or seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
