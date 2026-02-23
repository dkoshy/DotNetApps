using BethaniesPieShope.DBAccess;
using BethaniesPieShope.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Register services here
var services = builder.Services;
services.AddControllersWithViews();
services.AddDbContext<BethaniesPieDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

services.AddScoped<IPieRepository, PieRepository>()
        .AddScoped<ICategoryRepository, CategoryRepository>();

var app = builder.Build();

//Configure the HTTP request pipeline here (middleware, endpoints, etc.)
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
