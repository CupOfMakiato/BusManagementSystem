using Microsoft.EntityFrameworkCore;
using Stripe;
using SystemDAO;
using SystemRepository.Implementation;
using SystemRepository.Interface;
using SystemService.Implementation;
using SystemService.Interface;
using SystemRepository;
using SystemService;

var builder = WebApplication.CreateBuilder(args);


<<<<<<< HEAD
=======
// Register BusManagementSystemContext with a connection string
builder.Services.AddDbContext<BusManagementSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services as Scoped instead of Singleton
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IDriverService, DriverService>();

>>>>>>> master

// register services
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBusService, BusService>();
builder.Services.AddSingleton<IRoleService, RoleService>();
builder.Services.AddSingleton<IRouteService, RouteService>();
builder.Services.AddSingleton<IDriverService, DriverService>();
builder.Services.AddSingleton<IFreeTicketService, FreeTicketService>();
builder.Services.AddSingleton<IFreeTicketVerificationService, FreeTicketVerificationService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();


builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IBusRepository, BusRepository>();
builder.Services.AddSingleton<IRoleRepository, RoleRepository>();
builder.Services.AddSingleton<IDriverRepository, DriverRepository>();
builder.Services.AddSingleton<IRouteRepository, RouteRepository>();
builder.Services.AddSingleton<IFreeTicketRepository, FreeTicketRepository>();
builder.Services.AddSingleton<IFreeTicketVerificationRepository, FreeTicketVerificationRepository>();




//builder.Services.AddScoped<UserDAO>();



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
