using SystemRepository.Implementation;
using SystemRepository.Interface;
using SystemService.Implementation;
using SystemService.Interface;

var builder = WebApplication.CreateBuilder(args);

// register services
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBusService, BusService>();


builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IBusRepository, BusRepository>();



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

app.UseAuthorization();

app.MapRazorPages();

app.UseSession();

app.Run();
