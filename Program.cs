using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zwemclub.Data;
using Zwemclub.Data.UnitOfWork;
using Zwemclub.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

// Database
builder.Services.AddDbContext<ZwemclubContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LocalDBConnection")
        ?? throw new InvalidOperationException(
            "ConnectionStrings:LocalDBConnection ontbreekt."
        )
    )
);

// Identity
builder.Services.AddIdentity<CustomUser, IdentityRole>()
    .AddEntityFrameworkStores<ZwemclubContext>()
    .AddDefaultTokenProviders();

// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Cookie configuratie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Gebruiker/Login";
    options.AccessDeniedPath = "/Home/ToegangGeweigerd";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Standaard naar Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();