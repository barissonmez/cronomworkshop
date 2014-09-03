using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cronom.Web.Data;
using Cronom.Web.Domains;
using Cronom.Web.Domains.Enums;
using Cronom.Web.Helpers;
using Cronom.Web.Models;
using Cronom.Web.Services.Contracts;

namespace Cronom.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserService _userService;
        private readonly IRentalService _rentalService;
        private readonly IDashboardService _dashboardService;

        public HomeController(IUserService userService, IRentalService rentalService, IDashboardService dashboardService)
        {
            _userService = userService;
            _rentalService = rentalService;
            _dashboardService = dashboardService;
        }

        public ActionResult Index()
        {
 
            switch (CurrentUser.Identity.UserType)
            {
                case UserType.Manager:
                    return RedirectToAction("Index", "Manager");
                case UserType.Student:
                    return RedirectToAction("Index", "Student");
                case UserType.SuperAdmin:
                    break;
            }

            var model = _dashboardService.GetStats();
            return View(model);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(NewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userService.IsUserNameExist(model.UserName))
                {
                    ModelState.AddModelError("", "Bu kullanıcı adıyla kayıtlı başka bir kullanıcı bulunmaktadır. Lütfen başka bir kullanıcı adı seçiniz.");
                }
                else
                {
                    model.UserType = UserType.Manager;

                    if (_userService.SaveUser(model))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bir sorun oluştu. Lütfen tekrar deneyin.");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Kayıt için gerekli bütün alanları doldurduğunuzdan emin olunuz.");
            }

            return View(model);
        }

        public ActionResult StockReport()
        {
            return View();
        }


        public ActionResult MostRentedBooksReport()
        {
            var model = _rentalService.GetPager(null, null);
            return View(model);
        }

        public ActionResult MostRentedBooks(int page, string dateRange)
        {
            var model = _rentalService.GetPager(page, dateRange);
            return PartialView(model);
        }


    }
}