using Microsoft.AspNetCore.Mvc;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Domain;

namespace SanTsgHotelBooking.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourVisioAPIService _tourVisioAPIService;
        public const string JWTKeyName = "_token";

        public SearchController(ILogger<SearchController> logger, IUnitOfWork unitOfWork, ITourVisioAPIService tourVisioAPIService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _tourVisioAPIService = tourVisioAPIService;
            
        }

        public IActionResult Index()
        {
            IEnumerable<City> cities = _unitOfWork.Cities.GetAll().ToList();

            return View(cities);
        }

        [HttpGet]
        public async Task<IActionResult> Hotels(string searchString)
        {
            IEnumerable<HotelProduct> hotels = new List<HotelProduct>();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(JWTKeyName)))
            {
                HttpContext.Session.SetString(JWTKeyName, await _tourVisioAPIService.LoginTourVisio());
                string? token = HttpContext.Session.GetString(JWTKeyName);
                _logger.LogInformation("Tourvisio login token:" + token);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                string? token = HttpContext.Session.GetString(JWTKeyName);

                hotels =  await _tourVisioAPIService.SearchHotels(searchString,token);
                return View(hotels);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            string? token = HttpContext.Session.GetString(JWTKeyName);
            HotelDetails hotelDetails = await _tourVisioAPIService.GetHotelDetails(id??0, token);

            return View(hotelDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AutoComplete(string term)
        {
            var cities = _unitOfWork.Cities.GetAll(u => u.CityName.Contains(term)).Select(u => u.CityName).ToList();
            return Json(cities);
        }

    }
}
