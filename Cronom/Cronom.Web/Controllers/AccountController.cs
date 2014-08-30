using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cronom.Web.Data;
using Cronom.Web.Domains;
using Cronom.Web.Domains.Enums;
using Cronom.Web.Models;
using Cronom.Web.Services.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Cronom.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
 
                var user = _userService.GetUser(model.UserName, model.Password);

                if (user != null)
                {

                    var identity = new ClaimsIdentity(new[] 
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Role, user.UserType.ToString()),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.GivenName, user.FullName),
                        },
                        DefaultAuthenticationTypes.ApplicationCookie,
                        ClaimTypes.Name, 
                        ClaimTypes.Role);
                    
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    }, identity);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Girmiş olduğunuz Kullanıcı Adı veya Şifre geçersiz.");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}