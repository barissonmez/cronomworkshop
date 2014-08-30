using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.WebPages;
using Cronom.Web.Domains;
using Cronom.Web.Domains.Enums;

namespace Cronom.Web.Helpers
{
	public class CurrentUser 
	{
        public static ApplicationUser Identity
        {
            
            get
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var claims = identity.Claims.ToList();

                if (PassedValidation(claims))
                {
                    return new ApplicationUser
                                {
                                    Id = Guid.Parse(claims.FirstOrDefault(a => a.Type.Equals(ClaimTypes.NameIdentifier)).Value),
                                    FullName = claims.FirstOrDefault(a => a.Type.Equals(ClaimTypes.GivenName)).Value,
                                    UserType = (UserType)Enum.Parse(typeof(UserType), claims.FirstOrDefault(a => a.Type.Equals(ClaimTypes.Role)).Value),
                                    UserName = claims.FirstOrDefault(a => a.Type.Equals(ClaimTypes.Name)).Value
                                };
                }

                return new ApplicationUser();
                
            }
        }


	    private static bool PassedValidation(List<Claim> claims)
	    {
            if (claims.FirstOrDefault(a => a.Type.Equals(ClaimTypes.NameIdentifier)) == null) return false;
            if (claims.FirstOrDefault(a => a.Type.Equals(ClaimTypes.Role)) == null) return false;
            if (claims.FirstOrDefault(a => a.Type.Equals(ClaimTypes.Name)) == null) return false;
            if (claims.FirstOrDefault(a => a.Type.Equals(ClaimTypes.GivenName)) == null) return false;

	        return true;
	    }
	}
}