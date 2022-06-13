using Microsoft.AspNetCore.Mvc;
using SanTsgHotelBooking.Application.Models.GetArrivalAutocompleteResponse;
using SanTsgHotelBooking.Application.Models.GetProductInfoResponse;
using SanTsgHotelBooking.Application.Models.LocationHotelPriceResponse;
using SanTsgHotelBooking.Application.Models.TourVisioLoginResponse;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Data.Repository.IRepository;

namespace SanTsgHotelBooking.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISanTsgTourVisioService _sanTsgTourVisioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public const string JWTKeyName = "_token";

        public SearchController(ILogger<SearchController> logger, IUnitOfWork unitOfWork, ISanTsgTourVisioService sanTsgTourVisioService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _sanTsgTourVisioService = sanTsgTourVisioService;
        }

        public IActionResult Index()
        {
            IEnumerable<Domain.City> cities = _unitOfWork.Cities.GetAll().ToList();
            _logger.LogInformation("List of all cities returned");
            return View(cities);
        }

        [HttpGet]
        public async Task<IActionResult> Hotels(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString) && searchString.Length >= 3)
            {
                string token = await GetSanTsgTourVisioToken();
                int arrivalLocId = 0;
                var response = await _sanTsgTourVisioService.GetArrivalAutoCompleteAsync<GetArrivalAutocompleteResponse>(searchString, token);
                if (response != null && response.Header.success)
                {
                    for (int i = 0; i < response.Body.items.Count; i++)
                    {
                        if (response.Body.items[i].city != null)
                        {
                            if (string.Equals(response.Body.items[i].city.name, searchString, StringComparison.OrdinalIgnoreCase))
                            {
                                arrivalLocId = Int32.Parse(response.Body.items[i].city.id);
                            }
                        }
                    }
                }
                if (arrivalLocId > 0)
                {
                    ViewData["CityID"] = arrivalLocId;
                    return View();
                }
                else { return RedirectToAction("Index"); }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> HotelDetails(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            string token = await GetSanTsgTourVisioToken();
            var hotelResponse = await _sanTsgTourVisioService.GetHotelDetailsByIdAsync<GetProductInfoResponse>(id, token);
            if (hotelResponse != null && hotelResponse.header.success && hotelResponse.body.hotel != null)
            {
                return View(hotelResponse.body.hotel);
            }
            return RedirectToAction("Index");
        }



        #region APICALLS

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GetCertainHotelPrice(int hotelId, int adultNumber)
        {
            if (hotelId != 0)
            {
                string token = await GetSanTsgTourVisioToken();
                var hotelResponse = await _sanTsgTourVisioService.GetHotelPriceAsync<LocationHotelPriceResponse>(hotelId, adultNumber, token);
                if (hotelResponse != null && hotelResponse.header.success && hotelResponse.body.hotels != null)
                {
                    var hotelVMs = hotelResponse.body.hotels;
                    return Json(new { data = hotelVMs });
                }
            }
            return Json("Search problem...");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GetHotelsPrices(string term)
        {
            if (!String.IsNullOrEmpty(term) && term.Length >= 3)
            {
                string token = await GetSanTsgTourVisioToken();
                int arrivalLocId = int.Parse(term);
                var hotelResponse = await _sanTsgTourVisioService.LocationHotelPriceSearchAsync<LocationHotelPriceResponse>(arrivalLocId, token);
                if (hotelResponse != null && hotelResponse.header.success && hotelResponse.body.hotels != null)
                {
                    var hotelVMs = hotelResponse.body.hotels;
                    return Json(new { data = hotelVMs });
                }
            }
            return Json("Search problem...");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AutoComplete(string term)
        {
            var cities = _unitOfWork.Cities.GetAll(u => u.CityName.Contains(term)).Select(u => u.CityName).ToList();
            return Json(cities);
        }

        #endregion

        //I need to refactor this part for SOLID princ
        private async Task<string> GetSanTsgTourVisioToken()
        {
            //string cookieValueFromReq = Request.Cookies[JWTKeyName];
            string cookieValueFromReq = _httpContextAccessor.HttpContext.Request.Cookies[JWTKeyName];
            if (string.IsNullOrEmpty(cookieValueFromReq))
            {
                string newToken = "";
                var response = await _sanTsgTourVisioService.AuthLoginTourVisioAsync<TourVisioLoginResponse>();
                if (response != null && response.Header.success)
                {
                    newToken = response.Body.token;
                    //HttpContext.Session.SetString(JWTKeyName, newToken);
                    _logger.LogInformation("Tourvisio new login requested token:" + newToken);
                    CookieSet(JWTKeyName, newToken, response.Body.expiresOn);
                }
                else
                {
                    _logger.LogInformation("Couldn't retrieve token" + DateTime.Now);
                }
            }
            //string token = HttpContext.Session.GetString(JWTKeyName);
            return cookieValueFromReq;
        }

        private void CookieSet(string key, string value, DateTime? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = expireTime;
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }
    }
}
