using Microsoft.EntityFrameworkCore;
using SanTsgHotelBooking.Application.Services;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Data;
using SanTsgHotelBooking.Data.DbInitializer;
using SanTsgHotelBooking.Data.Repository;
using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Shared.SettingsModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IEmailService, EmailService>();

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