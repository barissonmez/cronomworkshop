using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cronom.Web.Domains;
using Cronom.Web.Models;
using Cronom.Web.Services.Contracts;
using FastMapper;

namespace Cronom.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IRentalService _rentalService;
        private readonly IDashboardService _dashboardService;

        public StudentController(IBookService bookService, IRentalService rentalService, IDashboardService dashboardService)
        {
            _bookService = bookService;
            _rentalService = rentalService;
            _dashboardService = dashboardService;
        }

        public ActionResult Index()
        {
            var model = _dashboardService.GetStats();
            return View(model);
        }

        public ActionResult BookList()
        {
            var books = _bookService.GetBooks();

            var bookListModel = TypeAdapter.Adapt<List<Book>, List<BookViewModel>>(books);

            return View(bookListModel);
        }

        public ActionResult Requests()
        {
            var model = _rentalService.GetRentalRequests();
            return View(model);
        }

        [HttpPost]
        public ActionResult Cancel(RequestCheckInModel model)
        {
            var result = _rentalService.Cancel(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(string slug)
        {
            var book = _bookService.GetBookBySlug(slug);
            var model = TypeAdapter.Adapt<Book, BookViewModel>(book);

            return View(model);
        }

        [HttpPost]
        public JsonResult Rent(RentalInModel model)
        {
            var result = _rentalService.RentBook(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}