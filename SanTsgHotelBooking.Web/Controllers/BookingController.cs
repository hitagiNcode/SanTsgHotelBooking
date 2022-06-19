using Microsoft.AspNetCore.Mvc;
using SanTsgHotelBooking.Application.Models.BeginTransactionResponse;
using SanTsgHotelBooking.Application.Models.CommitTransaction.Response;
using SanTsgHotelBooking.Application.Models.GetReservationDetail.Response;
using SanTsgHotelBooking.Application.Models.SetReservationInfo.Response;
using SanTsgHotelBooking.Application.Models.TourVisioLoginResponse;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Domain;
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
                    _logger.LogInformation("transactionid: " + beginTransResponse.body.transactionId);
                    PassangerInfoVM newPassanger = new() { transactionId = beginTransResponse.body.transactionId };
                    return View(newPassanger);
                }
            }

            return View();
        }

        public async Task<IActionResult> CompleteBooking(PassangerInfoVM passangerInfo)
        {
            if (passangerInfo != null)
            {
                string token = await GetSanTsgTourVisioToken();
                var setResInfoResponse = await _sanTsgTourVisioService.SetReservationInfoAsync<SetReservationInfoResponse>(
                    passangerInfo.transactionId, passangerInfo.FirstName,
                    passangerInfo.LastName, email: passangerInfo.Email, token);
                if (setResInfoResponse.body != null && setResInfoResponse.header.success)
                {
                    _logger.LogInformation("Set Info Transactionid: " + setResInfoResponse.body.transactionId);
                    var commitTransResponse = await _sanTsgTourVisioService.CommitTransactionAsync<CommitTransactionResponse>(setResInfoResponse.body.transactionId, token);
                    if (commitTransResponse.body != null && setResInfoResponse.header.success)
                    {
                        _logger.LogInformation("Success! Reservation Number: " + commitTransResponse.body.reservationNumber);
                        var getReservationDetail = await _sanTsgTourVisioService.GetReservationDetailAsync<GetReservationDetailResponse>(commitTransResponse.body.reservationNumber, token);
                        if (getReservationDetail.body != null && getReservationDetail.header.success)
                        {
                            _logger.LogInformation("Reservation Details: " + getReservationDetail.body.reservationNumber);
                            Booking newBooking = new Booking()
                            {
                                ReservationNumber = getReservationDetail.body.reservationNumber,
                                FirstName = passangerInfo.FirstName,
                                LastName = passangerInfo.LastName,
                                Email = passangerInfo.Email,
                                Price = getReservationDetail.body.reservationData.reservationInfo.salePrice.amount
                            };
                            _unitOfWork.Bookings.Add(newBooking);
                            _unitOfWork.Save();
                            return View(getReservationDetail.body);
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Bookings()
        {
            IEnumerable<Booking> bookingList = _unitOfWork.Bookings.GetAll();
            return View(bookingList);
        }

        //I need to refactor this part for SOLID princ
        private async Task<string> GetSanTsgTourVisioToken()
        {
            string cookieValueFromReq = _httpContextAccessor.HttpContext.Request.Cookies[JWTKeyName];
            if (string.IsNullOrEmpty(cookieValueFromReq))
            {
                string newToken = "";
                var response = await _sanTsgTourVisioService.AuthLoginTourVisioAsync<TourVisioLoginResponse>();
                if (response != null && response.Header.success)
                {
                    newToken = response.Body.token;
                    _logger.LogInformation("Tourvisio new login requested token:" + newToken);
                    CookieSet(JWTKeyName, newToken, response.Body.expiresOn);
                }
                else
                {
                    _logger.LogInformation("Couldn't retrieve token" + DateTime.Now);
                }
            }
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
