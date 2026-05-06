using Globomantics.Domain.Models;
using Globomantics.Infrastructure.Data;
using Globomatics.Infra.Data;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Constraints;
using Globomatics.Web.Filters;
using Globomatics.Web.Implimetataions;
using Globomatics.Web.Transformers;
using Globomatics.Web.ValueProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GlobalManticsIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'GlobalManticsIdentityContextConnection' not found.");

builder.Services.AddControllersWithViews(opt =>
{
    opt.ValueProviderFactories.Add(new SessionValueProviderFactory());
});
builder.Services.AddRouting(opt =>
{
    opt.ConstraintMap.Add("slugvalue", typeof(SlugConstraint));
    opt.ConstraintMap.Add("slugtrasform", typeof(SlugParameterTrsformer));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddDbContext<GlobalManticsIdentityContext>(ServiceLifetime.Scoped);
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<GlobalManticsIdentityContext>();
builder.Services.AddDbContext<GlobomanticsContext>(ServiceLifetime.Scoped);


//Application Services
builder.Services.AddTransient<IStateRepository, SessionstateRepository>();

var services = builder.Services;
services.AddScoped<ICartRepository, CartRepository>()
    .AddScoped<IRepository<Customer>, CustomerRepository>()
    .AddScoped<IRepository<Product>, ProductRepository>()
    .AddScoped<IRepository<Order>, OrderRepository>()
    .AddScoped<IRepository<Cart>, CartRepository>()
    .AddScoped<TimerFilter>();



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

app.UseAuthentication();
app.UseAuthorization();
/*
app.MapControllerRoute(
    name: "ticketdetails",
    defaults: new { controller = "Home", action = "TicketDetails" },
    pattern: "details/{productId:guid}/{slug?}"
    );*/
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();