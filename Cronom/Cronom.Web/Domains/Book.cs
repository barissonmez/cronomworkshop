using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cronom.Web.Domains
{
    public class Book
    {
        public Book()
        {
            Rentals = new List<Rental>();
        }

        [Key]
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}