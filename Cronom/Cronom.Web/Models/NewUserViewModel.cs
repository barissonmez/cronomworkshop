using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Cronom.Web.Domains.Enums;

namespace Cronom.Web.Models
{
    public class NewUserViewModel
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; }

        public UserType UserType { get; set; }
    }
}