using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Data.DBInteractions.Contracts;
using Cronom.Web.Domains;
using Cronom.Web.Services.Contracts;

namespace Cronom.Web.Services.Implementations
{
    public class UserService : AbstractBaseService, IUserService
    {
        private readonly IRepository<User> _repo;
        private readonly IUnitOfWork _uow;

        public UserService(IRepository<User> repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public User GetUser(string username, string password)
        {
            return _repo.Get(a => a.UserName.Equals(username) && a.Password.Equals(password));
        }


        public bool SaveUser(Models.NewUserViewModel model)
        {
            var user = new User()
            {
                FullName = model.FullName,
                Password = model.Password,
                UserName = model.UserName,
                UserType = model.UserType
            };

            _repo.Add(user);
            return _uow.Commit();

        }


        public bool IsUserNameExist(string username)
        {
            return _repo.Get(a=> a.UserName.ToLowerInvariant().Equals(username.ToLowerInvariant())) != null;

        }
    }
}