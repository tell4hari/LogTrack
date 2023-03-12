using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LogTracker.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LogTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LogTrackerContext") ?? throw new InvalidOperationException("Connection string 'LogTrackerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
   // pattern: "{controller=Product}/{action=Index}/{id?}");
pattern: "{controller=User}/{action=Create}/{id?}");

app.Run();
