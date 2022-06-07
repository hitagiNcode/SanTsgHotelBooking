using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SanTsgHotelBooking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(ApplicationDbContext db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Initialize()
        {
            /*try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Init exception occured" + ex);
            }*/

            if (_db.Cities.Any())
            {
                return;
            }

            var cities = new City[]
            {
                new City{CityName="afyon", CountryCode="TR"},
                new City{CityName="antalya", CountryCode="TR"},
                new City{CityName="antakya", CountryCode="TR"},
                new City{CityName="istanbul", CountryCode="TR"},
                new City{CityName="izmir", CountryCode="TR"},
                new City{CityName="ankara", CountryCode="TR"},

            };

            foreach(City city in cities)
            {
                _db.Cities.Add(city);
            }

            _db.SaveChanges();
        }
    }
}
