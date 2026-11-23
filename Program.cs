var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

builder.Services.AddDbContext<ZwemclubContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDBConnection")
        ?? throw new InvalidOperationException("ConnectionStrings:LocalDBConnection ontbreekt.")));

builder.Services.AddIdentity<CustomUser, IdentityRole>()
    .AddEntityFrameworkStores<ZwemclubContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Gebruiker/Login";
    options.AccessDeniedPath = "/Home/ToegangGeweigerd";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
