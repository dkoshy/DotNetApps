using BethaniesPieShope.DBAccess;
using BethaniesPieShope.Models;
using BethaniesPieShope.Models.Contracts;
using BethaniesPieShope.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Register services here
var services = builder.Services;
services.AddControllersWithViews();
services.AddRazorPages();
services.AddHttpContextAccessor();
services.AddSession();
services.AddDbContext<BethaniesPieDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<BethaniesPieDbContext>();
services.AddHttpClient();
services.AddScoped<IShoppingCart , ShoppingCart>(sp => ShoppingCart.CreateACart(sp));
services.AddScoped<IPieRepository, PieRepository>()
        .AddScoped<ICategoryRepository, CategoryRepository>()
        .AddScoped<IOrderReopository, OrderRepository>();

var app = builder.Build();

//seed the database here
//DbDataInitializer.SeedData(app);

//Configure the HTTP request pipeline here (middleware, endpoints, etc.)
app.UseStaticFiles();

app.UseAuthentication();

app.UseSession();
//app.MapDefaultControllerRoute(); 
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id:int?}");

app.MapRazorPages();
app.Run();
