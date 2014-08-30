using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Cronom.Web.Domains.Enums;

namespace Cronom.Web.Models
{
    public class BookViewModel
    {
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public string Slug { get; set; }
    }
}