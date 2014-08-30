using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronom.Web.Models
{
    public class StockOutModel
    {
        public StockOutModel()
        {
            Books = new List<BookViewModel>();
        }

        public List<BookViewModel> Books { get; set; }
        public int Count { get; set; }

        
    }
}