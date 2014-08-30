using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Domains;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Cronom.Web
{
	public partial class Startup
	{

	    public void ConfigureAuth(IAppBuilder app)
	    {
	        // Enable the application to use a cookie to store information for the signed in user
	        app.UseCookieAuthentication(new CookieAuthenticationOptions
	        {
	            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
	            LoginPath = new PathString("/Account/Login")
	        });

           app.UseExternalSignInCookie();
	    }

        
	}
}