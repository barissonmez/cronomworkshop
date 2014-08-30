using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Cronom.Web.Models;
using Cronom.Web.Services.Contracts;

namespace Cronom.Web.Controllers
{
    
    public class StockController : ApiController
    {
        private readonly IBookService _bookService;

        public StockController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [System.Web.Http.HttpGet]
        public StockOutModel Get()
        {
            return _bookService.GetStock();

        }
    }
}
