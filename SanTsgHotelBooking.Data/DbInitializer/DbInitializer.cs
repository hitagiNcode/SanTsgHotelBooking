using Microsoft.Extensions.Logging;
using SanTsgHotelBooking.Domain;

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
                new City{CityName="adana", CountryCode="TR"},
                new City{CityName="adiyaman", CountryCode="TR"},
                new City{CityName="afyonkarahisar", CountryCode="TR"},
                new City{CityName="agri", CountryCode="TR"},
                new City{CityName="amasya", CountryCode="TR"},
                new City{CityName="ankara", CountryCode="TR"},
                new City{CityName="antalya", CountryCode="TR"},
                new City{CityName="artvin", CountryCode="TR"},
                new City{CityName="aydin", CountryCode="TR"},
                new City{CityName="balikesir", CountryCode="TR"},
                new City{CityName="bilecik", CountryCode="TR"},
                new City{CityName="bingol", CountryCode="TR"},
                new City{CityName="bitlis", CountryCode="TR"},
                new City{CityName="bolu", CountryCode="TR"},
                new City{CityName="burdur", CountryCode="TR"},
                new City{CityName="bursa", CountryCode="TR"},
                new City{CityName="canakkale", CountryCode="TR"},
                new City{CityName="cankiri", CountryCode="TR"},
                new City{CityName="corum", CountryCode="TR"},
                new City{CityName="denizli", CountryCode="TR"},
                new City{CityName="diyarbakir", CountryCode="TR"},
                new City{CityName="edirne", CountryCode="TR"},
                new City{CityName="elazig", CountryCode="TR"},
                new City{CityName="erzincan", CountryCode="TR"},
                new City{CityName="erzurum", CountryCode="TR"},
                new City{CityName="eskisehir", CountryCode="TR"},
                new City{CityName="gaziantep", CountryCode="TR"},
                new City{CityName="giresun", CountryCode="TR"},
                new City{CityName="gümüshane", CountryCode="TR"},
                new City{CityName="hakkari", CountryCode="TR"},
                new City{CityName="hatay", CountryCode="TR"},
                new City{CityName="isparta", CountryCode="TR"},
                new City{CityName="mersin", CountryCode="TR"},
                new City{CityName="istanbul", CountryCode="TR"},
                new City{CityName="izmir", CountryCode="TR"},
                new City{CityName="kars", CountryCode="TR"},
                new City{CityName="kastamonu", CountryCode="TR"},
                new City{CityName="kayseri", CountryCode="TR"},
                new City{CityName="kirklareli", CountryCode="TR"},
                new City{CityName="kirsehir", CountryCode="TR"},
                new City{CityName="kocaeli", CountryCode="TR"},
                new City{CityName="konya", CountryCode="TR"},
                new City{CityName="kutahya", CountryCode="TR"},
                new City{CityName="malatya", CountryCode="TR"},
                new City{CityName="manisa", CountryCode="TR"},
                new City{CityName="kahramanmaras", CountryCode="TR"},
                new City{CityName="mardin", CountryCode="TR"},
                new City{CityName="mugla", CountryCode="TR"},
                new City{CityName="mus", CountryCode="TR"},
                new City{CityName="nevsehir", CountryCode="TR"},
                new City{CityName="nigde", CountryCode="TR"},
                new City{CityName="ordu", CountryCode="TR"},
                new City{CityName="rize", CountryCode="TR"},
                new City{CityName="sakarya", CountryCode="TR"},
                new City{CityName="samsun", CountryCode="TR"},
                new City{CityName="siirt", CountryCode="TR"},
                new City{CityName="sinop", CountryCode="TR"},
                new City{CityName="sivas", CountryCode="TR"},
                new City{CityName="tekirdag", CountryCode="TR"},
                new City{CityName="tokat", CountryCode="TR"},
                new City{CityName="trabzon", CountryCode="TR"},
                new City{CityName="tunceli", CountryCode="TR"},
                new City{CityName="sanliurfa", CountryCode="TR"},
                new City{CityName="usak", CountryCode="TR"},
                new City{CityName="van", CountryCode="TR"},
                new City{CityName="yozgat", CountryCode="TR"},
                new City{CityName="zonguldak", CountryCode="TR"},
                new City{CityName="aksaray", CountryCode="TR"},
                new City{CityName="bayburt", CountryCode="TR"},
                new City{CityName="karaman", CountryCode="TR"},
                new City{CityName="kirikkale", CountryCode="TR"},
                new City{CityName="batman", CountryCode="TR"},
                new City{CityName="sirnak", CountryCode="TR"},
                new City{CityName="bartin", CountryCode="TR"},
                new City{CityName="ardahan", CountryCode="TR"},
                new City{CityName="igdir", CountryCode="TR"},
                new City{CityName="yalova", CountryCode="TR"},
                new City{CityName="karabuk", CountryCode="TR"},
                new City{CityName="kilis", CountryCode="TR"},
                new City{CityName="osmaniye", CountryCode="TR"},
                new City{CityName="duzce", CountryCode="TR"},

            };

            foreach(City city in cities)
            {
                _db.Cities.Add(city);
            }

            _db.SaveChanges();
        }
    }
}
