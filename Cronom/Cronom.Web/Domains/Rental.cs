using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Domains.Enums;

namespace Cronom.Web.Domains
{
    public class Rental : EntityBase
    {
        public Book Book { get; set; }
        public string BookISBN { get; set; }
        public User RentedBy { get; set; }
        public Guid RentedById { get; set; }
        public RentalState State { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}