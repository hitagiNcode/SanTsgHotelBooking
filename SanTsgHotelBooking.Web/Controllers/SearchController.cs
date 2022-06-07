using Microsoft.AspNetCore.Mvc;
using SanTsgHotelBooking.Data.Repository.IRepository;

namespace SanTsgHotelBooking.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public SearchController(ILogger<SearchController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                //var hotelProducts = _unitOfWork.HotelProducts.GetAll(u => u.City.Contains(searchString)).ToList();
                return View();
            }

            return View();
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
