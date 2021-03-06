using Microsoft.EntityFrameworkCore;
using SanTsgHotelBooking.Application.Services;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Data;
using SanTsgHotelBooking.Data.DbInitializer;
using SanTsgHotelBooking.Data.Repository;
using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Shared.SettingsModels;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

//Serilog settings
var logger = new LoggerConfiguration()
  .WriteTo.Console()
  .WriteTo.File(new JsonFormatter(), "logs-{Date}.json", rollingInterval: RollingInterval.Day)
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

/*builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));*/
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
    HerokuPostgreSQLSettings.GetHerokuConnectionString(builder.Configuration.GetConnectionString("DATABASE_URL"))
));
//Allows legacy datetime usage
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddHttpClient();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<TourvisioAPISettings>(builder.Configuration.GetSection("TourVisioApi"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISanTsgTourVisioService, SanTsgTourVisioService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
SeedDatabase();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}