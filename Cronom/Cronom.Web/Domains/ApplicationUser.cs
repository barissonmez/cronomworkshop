using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cronom.Web.Domains.Enums;

namespace Cronom.Web.Domains
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public UserType UserType { get; set; }
    }
}
