using Microsoft.AspNetCore.Mvc;
using SanTsgHotelBooking.Application.Models;
using SanTsgHotelBooking.Application.Services.IServices;
using SanTsgHotelBooking.Data.Repository.IRepository;
using SanTsgHotelBooking.Domain;
using SanTsgHotelBooking.Web.Models;
using System.Diagnostics;

namespace SanTsgHotelBooking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            IEnumerable<DumUser> userList = _unitOfWork.DumUser.GetAll();
            return View(userList);
        }

        #region Create User Region
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DumUser obj)
        {
            if (obj.isUserActive == false)
            {
                ModelState.AddModelError("isActive", "Can't add users as inactive.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.DumUser.Add(obj);
                _unitOfWork.Save();

                MailRequest mail = new MailRequest()
                {
                    Body = "Kullanici Kaydiniz basariyla alinmistir. " + obj.UserName + " teşekkür ederiz.",
                    Subject = "SanTsg Kulanici kaydi",
                    ToEmail = obj.Email
                };
                await _emailService.SendEmailAsync(mail);

                TempData["success"] = "User created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        #endregion

        #region Edit User Region
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userList = _unitOfWork.DumUser.GetFirstOrDefault(u => u.Id == id);

            if (userList == null)
            {
                return NotFound();
            }

            return View(userList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DumUser obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.DumUser.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "User updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        #endregion

        #region Delete User Region
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFromDbFirst = _unitOfWork.DumUser.GetFirstOrDefault(u => u.Id == id);

            if (userFromDbFirst == null)
            {
                return NotFound();
            }

            return View(userFromDbFirst);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(DumUser user)
        {
            var obj = _unitOfWork.DumUser.GetFirstOrDefault(u => u.Id == user.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.DumUser.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }

        //POST
        [HttpPost, ActionName("SoftDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDeletePOST(DumUser user)
        {
            var obj = _unitOfWork.DumUser.GetFirstOrDefault(u => u.Id == user.Id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.isUserDeleted = true;
            _unitOfWork.DumUser.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "User deleted softly";
            return RedirectToAction("Index");
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}