using Globomantics.Domain.Models;
using Globomantics.Infrastructure.Data;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Constraints;
using Globomatics.Web.Transformers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRouting(opt =>
{
    opt.ConstraintMap.Add("slugvalue", typeof(SlugConstraint));
    opt.ConstraintMap.Add("slugtrasform", typeof(SlugParameterTrsformer));
});

builder.Services.AddDbContext<GlobomanticsContext>(ServiceLifetime.Scoped);

//Application Services

var services = builder.Services;
services.AddScoped<ICartRepository,CartRepository>()
    .AddScoped<IRepository<Customer> , CustomerRepository>()
    .AddScoped<IRepository<Product> , ProductRepository>()
    .AddScoped<IRepository<Order> , OrderRepository>()
    .AddScoped<IRepository<Cart> , CartRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


/*
app.MapControllerRoute(
    name: "ticketdetails",
    defaults: new { controller = "Home", action = "TicketDetails" },
    pattern: "details/{productId:guid}/{slug?}"
    );*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();