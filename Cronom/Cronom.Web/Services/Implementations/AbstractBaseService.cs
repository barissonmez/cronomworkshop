using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Cronom.Web.Data.DBInteractions.Contracts;

namespace Cronom.Web.Services.Implementations
{
    public abstract class AbstractBaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        protected AbstractBaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected AbstractBaseService(){}

        public IUnitOfWork UnitOfWork { get { return _unitOfWork; }}
    }
}