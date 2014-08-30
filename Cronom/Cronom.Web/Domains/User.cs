using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Domains.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cronom.Web.Domains
{
    public class User : EntityBase
    {
        public User()
        {
            Rentals = new List<Rental>();
        }

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}