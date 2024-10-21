using Microsoft.EntityFrameworkCore;
using Stripe;
using SystemDAO;
using SystemRepository.Implementation;
using SystemRepository.Interface;
using SystemService.Implementation;
using SystemService.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BusManagementSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// register services
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBusService, BusService>();
builder.Services.AddSingleton<IRoleService, RoleService>();
builder.Services.AddSingleton<IRouteService, RouteService>();



builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IBusRepository, BusRepository>();
builder.Services.AddSingleton<IRoleRepository, RoleRepository>();
builder.Services.AddSingleton<IRouteRepository, RouteRepository>();


//builder.Services.AddSingleton<UserDAO>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24);
});



// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use session middleware before authorization
app.UseSession();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    // Map Razor pages
    endpoints.MapRazorPages();

    // Redirect root URL to Login page
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/Guest/Index");
    });
});

app.Run();
