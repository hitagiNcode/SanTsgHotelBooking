using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SanTsgHotelBooking.Application.Models.GetArrivalAutocompleteResponse;
using SanTsgHotelBooking.Application.Models.LocationHotelPriceResponse;
using SanTsgHotelBooking.Application.Models.TourVisioLoginResponse;
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
        private readonly ISanTsgTourVisioService _sanTsgTourVisioService;
        public const string JWTKeyName = "_token";

        public SearchController(ILogger<SearchController> logger, IUnitOfWork unitOfWork, ITourVisioAPIService tourVisioAPIService, ISanTsgTourVisioService sanTsgTourVisioService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _tourVisioAPIService = tourVisioAPIService;
            _sanTsgTourVisioService = sanTsgTourVisioService;
        }

        public IActionResult Index()
        {
            IEnumerable<Domain.City> cities = _unitOfWork.Cities.GetAll().ToList();

            return View(cities);
        }

        [HttpGet]
        public async Task<IActionResult> Hotels(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString) && searchString.Length >= 3)
            {
                int arrivalLocId = 0;
                bool isArrivalLocFound = false;
                if (!isArrivalLocFound)
                {
                    var response = await _sanTsgTourVisioService.GetArrivalAutoCompleteAsync<GetArrivalAutocompleteResponse>(searchString, await GetSanTsgTourVisioToken());
                    if (response != null && response.Header.success)
                    {
                        for (int i = 0; i < response.Body.items.Count; i++)
                        {
                            if (response.Body.items[i].city != null)
                            {
                                if (string.Equals(response.Body.items[i].city.name, searchString, StringComparison.OrdinalIgnoreCase))
                                {
                                    arrivalLocId = Int32.Parse(response.Body.items[i].city.id);
                                    isArrivalLocFound = true;
                                }
                            }
                        }
                    }
                }

                IEnumerable<HotelProduct> hotels = new List<HotelProduct>();
                var hotelResponse = await _sanTsgTourVisioService.LocationHotelPriceSearchAsync<LocationHotelPriceResponse>(arrivalLocId, await GetSanTsgTourVisioToken());


                //hotels = await _tourVisioAPIService.SearchHotels(searchString, await GetSanTsgTourVisioToken());
                return View(hotels);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            HotelDetails hotelDetails = await _tourVisioAPIService.GetHotelDetails(id, await GetSanTsgTourVisioToken());

            return View(hotelDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AutoComplete(string term)
        {
            var cities = _unitOfWork.Cities.GetAll(u => u.CityName.Contains(term)).Select(u => u.CityName).ToList();
            return Json(cities);
        }

        /*private async Task<string> TourVisioToken()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(JWTKeyName)))
            {
                HttpContext.Session.SetString(JWTKeyName, await _tourVisioAPIService.LoginTourVisio());
                _logger.LogInformation("Tourvisio login token:" + HttpContext.Session.GetString(JWTKeyName));
            }
            string token = HttpContext.Session.GetString(JWTKeyName);

            return token;
        }*/

        private async Task<string> GetSanTsgTourVisioToken()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(JWTKeyName)))
            {
                string newToken = "";
                var response = await _sanTsgTourVisioService.AuthLoginTourVisioAsync<TourVisioLoginResponse>();
                if (response != null && response.Header.success)
                {
                    newToken = response.Body.token;
                    HttpContext.Session.SetString(JWTKeyName, newToken);
                }
                _logger.LogInformation("Tourvisio new login requested token:" + newToken);
            }
            string token = HttpContext.Session.GetString(JWTKeyName);
            return token;
        }
    }
}
