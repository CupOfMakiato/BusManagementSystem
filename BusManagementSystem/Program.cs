using Microsoft.EntityFrameworkCore;
using SystemDAO;
using SystemRepository;
using SystemRepository.Implementation;
using SystemRepository.Interface;
using SystemService;
using SystemService.Implementation;
using SystemService.Interface;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
// Register BusManagementSystemContext with a connection string



=======
// Register services as Scoped instead of Singleton
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IDriverService, DriverService>();
>>>>>>> master

builder.Services.AddScoped<IBookingService, BookingService>();


// register services
<<<<<<< HEAD
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBusService, BusService>();
builder.Services.AddSingleton<IRoleService, RoleService>();
builder.Services.AddSingleton<IRouteService, RouteService>();
builder.Services.AddSingleton<IDriverService, DriverService>();
builder.Services.AddSingleton<IFreeTicketService, FreeTicketService>();

=======
>>>>>>> master


builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();




// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24);
});

// Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
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
