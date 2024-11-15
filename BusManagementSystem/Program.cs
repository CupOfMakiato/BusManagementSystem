using Microsoft.EntityFrameworkCore;
using Net.payOS;
using System.Net;
using System.Net.Mail;
using SystemDAO;
using SystemRepository;
using SystemRepository.Implementation;
using SystemRepository.Interface;
using SystemService;
using SystemService.Implementation;
using SystemService.Interface;

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));




var builder = WebApplication.CreateBuilder(args);

// Register services as Scoped instead of Singleton
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IDriverService, DriverService>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IFreeTicketService, FreeTicketService>();

builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(payOS);


// register services

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();

builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IFreeTicketRepository, FreeTicketRepository>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


// Email
builder.Services.AddSingleton(new SmtpClient("smtp.gmail.com")
{
    Port = 587,
    Credentials = new NetworkCredential("passswp@gmail.com", "nguyen0908"),
    EnableSsl = true
});

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
