using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Domains.Enums;

namespace Cronom.Web.Models
{
    public class RentalViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string RentedBy { get; set; }
        public RentalState State { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}