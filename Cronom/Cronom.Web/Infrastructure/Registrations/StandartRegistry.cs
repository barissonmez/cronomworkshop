using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cronom.Web.Data.DBInteractions.Contracts;
using Cronom.Web.Services.Implementations;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Cronom.Web.Infrastructure.Registrations
{
    public class StandartRegistry : Registry
    {
        public StandartRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AddAllTypesOf(typeof(IRepository<>));
                scan.AddAllTypesOf(typeof(AbstractBaseService));
                scan.WithDefaultConventions();
            });
        }
    }
}