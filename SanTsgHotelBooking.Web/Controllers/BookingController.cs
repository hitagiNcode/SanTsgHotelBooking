using Microsoft.AspNetCore.Mvc;
using SanTsgHotelBooking.Application.Models.BeginTransactionResponse;
using SanTsgHotelBooking.Application.Models.TourVisioLoginResponse;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Data.Repository.IRepository;

namespace SanTsgHotelBooking.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISanTsgTourVisioService _sanTsgTourVisioService;
        public const string JWTKeyName = "_token";
        public BookingController(ILogger<BookingController> logger, IUnitOfWork unitOfWork, ISanTsgTourVisioService sanTsgTourVisioService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _sanTsgTourVisioService = sanTsgTourVisioService;
        }

        public IActionResult Index(string offerId)
        {
            _logger.LogInformation(offerId);
            ViewData["OfferID"] = offerId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> BeginTransaction(string offerId)
        {
            if (!String.IsNullOrEmpty(offerId) && offerId.Length >= 5)
            {
                string token = await GetSanTsgTourVisioToken();
                var beginTransResponse = await _sanTsgTourVisioService.BeginTransactionAsync<BeginTransactionResponse>(offerId, token);
                if (beginTransResponse != null && beginTransResponse.header.success)
                {
                    _logger.LogInformation(beginTransResponse.body.transactionId);
                    return Json(beginTransResponse.body.transactionId);
                }
            }
            return Json("Bad request");
        }

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
                    _logger.LogInformation("Tourvisio new login requested token:" + newToken);

                }
                else
                {
                    _logger.LogInformation("Couldn't retrieve token" + DateTime.Now);
                }
            }
            string token = HttpContext.Session.GetString(JWTKeyName);
            return token;
        }
    }
}
