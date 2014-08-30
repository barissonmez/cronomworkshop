using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace Cronom.Web.Infrastructure.Registrations.RegistrationConventions
{
    public class ControllerConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.CanBeCastTo(typeof(Controller)) && !type.IsAbstract)
            {
                registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
            }
        }
    }
}