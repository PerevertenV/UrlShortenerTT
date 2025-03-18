using Microsoft.EntityFrameworkCore;
using UrlShortener.DataAccess;
using UrlShortener.DataAccess.Repository.IRepository;
using UrlShortener.DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using UrlShortener.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Register/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminRole", policy => policy.RequireRole(StaticData.RoleAdmin));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.MapFallbackToFile("/angular/{*path:nonfile}", "angular/dist/angular-app/browser/index.html");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
