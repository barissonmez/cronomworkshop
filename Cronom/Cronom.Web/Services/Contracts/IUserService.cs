using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cronom.Web.Domains;
using Cronom.Web.Models;

namespace Cronom.Web.Services.Contracts
{
    public interface IUserService
    {
        User GetUser(string username, string password);
        bool SaveUser(NewUserViewModel model);
        bool IsUserNameExist(string username);
    }
}
