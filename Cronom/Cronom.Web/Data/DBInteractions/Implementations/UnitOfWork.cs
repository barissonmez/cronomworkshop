using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Data.DBInteractions.Contracts;

namespace Cronom.Web.Data.DBInteractions.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CronomDBContext _dbContext;

        public UnitOfWork(CronomDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Commit()
        {
            return _dbContext.Commit();
        }
    }
}