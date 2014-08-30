using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cronom.Web.Controllers;
using Cronom.Web.Data;
using Cronom.Web.Infrastructure;
using Cronom.Web.Infrastructure.Registrations;
using Cronom.Web.Infrastructure.Tasks;
using Cronom.Web.Migrations;
using StructureMap;
using StructureMap.Graph;

namespace Cronom.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public IContainer Container
        {
            get
            {
                return (IContainer)HttpContext.Current.Items["_Container"];
            }
            set
            {
                HttpContext.Current.Items["_Container"] = value;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.EnsureInitialized(); 

            //WEBAPI JSON Output Config
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));
            //WEBAPI XML Output Config
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));

            //FastMApper Type Adapter Initialization
            TypeAdapterConfiguration.Initialize();


            DependencyResolver.SetResolver(new StructureMapDependencyResolver(() => Container ?? ObjectFactory.Container));
            
            ObjectFactory.Configure(cfg =>
            {
                cfg.AddRegistry(new StandartRegistry());
                cfg.AddRegistry(new ControllerRegistry());
                cfg.AddRegistry(new MVCRegistry());
                cfg.AddRegistry(new TaskRegistry());
            });

            using (var container = ObjectFactory.Container.GetNestedContainer())
            {
                foreach (var task in container.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                }

                foreach (var task in container.GetAllInstances<IRunAtStartup>())
                {
                    task.Execute();
                }
            }

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator(GlobalConfiguration.Configuration));
        }

        public void Application_BeginRequest()
        {
            Container = ObjectFactory.Container.GetNestedContainer();

            foreach (var task in Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {



            foreach (var task in Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }

            Exception exception = Server.GetLastError();
            Server.ClearError();

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("exception", exception);

            if (exception.GetType() == typeof(HttpException))
            {
                routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
            }
            else
            {
                routeData.Values.Add("statusCode", 500);
            }

            IController controller = new ErrorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            Response.End();
        }

        public void Application_EndRequest()
        {
            try
            {
                foreach (var task in
                    Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            finally
            {
                Container.Dispose();
                Container = null;
            }
        }
    }
}
