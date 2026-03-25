using BethaniesPieShope.DBAccess;
using BethaniesPieShope.Models;
using BethaniesPieShope.Models.Contracts;
using BethaniesPieShope.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Register services here
var services = builder.Services;
services.AddControllersWithViews();
services.AddHttpContextAccessor();
services.AddSession();
services.AddDbContext<BethaniesPieDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
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
app.UseSession();
//app.MapDefaultControllerRoute(); 
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id:int?}");    

app.Run();
