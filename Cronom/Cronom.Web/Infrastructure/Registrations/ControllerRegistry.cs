using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cronom.Web.Infrastructure.Registrations.RegistrationConventions;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Cronom.Web.Infrastructure.Registrations
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.With(new ControllerConvention());
            });
        }
    }
}