using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SanTsgHotelBooking.Domain;

namespace SanTsgHotelBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<DumUser> DumUsers { get; set; }
        public DbSet<City> Cities { get; set; }

    }
}
