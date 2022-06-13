using Microsoft.AspNetCore.Mvc;
using SanTsgHotelBooking.Application.Models.BeginTransactionResponse;
using SanTsgHotelBooking.Application.Models.TourVisioLoginResponse;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Web.Models;

namespace SanTsgHotelBooking.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISanTsgTourVisioService _sanTsgTourVisioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public const string JWTKeyName = "_token";
        public BookingController(ILogger<BookingController> logger, IUnitOfWork unitOfWork, ISanTsgTourVisioService sanTsgTourVisioService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _sanTsgTourVisioService = sanTsgTourVisioService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(string offerId)
        {
            if (!String.IsNullOrEmpty(offerId) && offerId.Length >= 5)
            {
                string token = await GetSanTsgTourVisioToken();
                var beginTransResponse = await _sanTsgTourVisioService.BeginTransactionAsync<BeginTransactionResponse>(offerId, token);
                if (beginTransResponse != null && beginTransResponse.header.success)
                {
                    _logger.LogInformation("transactionid: "+beginTransResponse.body.transactionId);
                    PassangerInfoVM newPassanger = new() { transactionId = beginTransResponse.body.transactionId };
                    return View(newPassanger);
                }
            }
            
            return View();
        }

        public async Task<IActionResult> FinishBooking(PassangerInfoVM passangerInfo)
        {


            return View();
        }

        /*
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
        }*/

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
