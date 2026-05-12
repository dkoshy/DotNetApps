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
builder.Services.AddRazorPages();
builder.Services.AddRouting(opt =>
{
    opt.ConstraintMap.Add("slugvalue", typeof(SlugConstraint));
    opt.ConstraintMap.Add("slugtrasform", typeof(SlugParameterTrsformer));
});

builder.Services.AddHttpContextAccessor();

//session configuration
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
    options.Cookie.Path = "/";
    options.Cookie.Name = "__Globomantics.Session";
    options.Cookie.MaxAge = TimeSpan.FromHours(1);
    options.IdleTimeout = TimeSpan.FromMinutes(20);


});

//identity cookie configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.Path = "/";
    options.Cookie.Name = "__Globomantics.Identity";
    options.Cookie.MaxAge = TimeSpan.FromHours(12);
    options.ExpireTimeSpan = TimeSpan.FromHours(12);
});

builder.Services.AddDbContext<GlobalManticsIdentityContext>(ServiceLifetime.Scoped);
builder.Services.AddDbContext<GlobomanticsContext>(ServiceLifetime.Scoped);

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<GlobalManticsIdentityContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    //rate limit failed login attempts.
    options.Lockout.MaxFailedAccessAttempts = 2;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
});

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
app.MapRazorPages();
app.Run();