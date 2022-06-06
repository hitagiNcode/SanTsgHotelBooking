﻿using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger , IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
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
        public IActionResult Create(DumUser obj)
        {
            if (obj.isUserActive == false)
            {
                ModelState.AddModelError("isDeleted","Can't add users as inactive.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.DumUser.Add(obj);
                _unitOfWork.Save();
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
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.DumUser.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
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

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(DumUser user)
        {
            var obj = _unitOfWork.DumUser.GetFirstOrDefault(u => u.Id == user.Id);
            if (obj == null)
            {
                return NotFound();
            }

            if (user.isUserDeleted == true)
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.DumUser.Update(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "User deleted softly";
                    return RedirectToAction("Index");
                }
            }

            _unitOfWork.DumUser.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "User deleted successfully";
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