using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cronom.Web.Domains.Enums;
using Cronom.Web.Models;
using Cronom.Web.Services.Contracts;

namespace Cronom.Web.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRentalService _rentalService;
        private readonly IDashboardService _dashboardService;

        public ManagerController(IUserService userService, IRentalService rentalService, IDashboardService dashboardService)
        {
            _userService = userService;
            _rentalService = rentalService;
            _dashboardService = dashboardService;
        }

        public ActionResult Index()
        {
            var model = _dashboardService.GetStats();
            return View(model);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Requests()
        {
            var model = _rentalService.GetPendingRequests();
            return View(model);
        }

        [HttpPost]
        public ActionResult Check(RequestCheckInModel model)
        {
            var result = _rentalService.Check(model);

            return Json(result, JsonRequestBehavior.AllowGet);
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
                    model.UserType = UserType.Student;

                    if (_userService.SaveUser(model))
                    {
                        return RedirectToAction("Index", "Manager");
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
            return View();
        }
    }
}