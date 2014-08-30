using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Cronom.Web.Domains.Enums;

namespace Cronom.Web.Models
{
    public class RentalInModel
    {
        public string ISBN { get; set; }
        public string StudentId { get; set; }
        public string FullDate { get; set; }
    }
}