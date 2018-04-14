using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using ZGW.GMS.Core;
using ZGW.GMS.Core.Mvc.ErrorHandling;
using ZGW.GMS.Core.Tasks;

namespace ZGW.GMS.Web
{
    public class Global : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new OMSHandleErrorAttribute());
        }
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            RegisterContainer();
            RegisterGlobalFilters(GlobalFilters.Filters);
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*allActiveReport}", new { allActiveReport = @".*\.ar7(/.*)?" });
        }
        /// <summary>
        /// IOC
        /// </summary>
        private void RegisterContainer()
        {
            try
            {
                ContainerBuilder builder = new ContainerBuilder();
                builder.RegisterModule(new ClientRegistration());
                IContainer container = builder.Build();
                ObjectContainer.SetContainer(container);
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
                BootStrapper.Start(HostTargets.Web);

            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void ExcuteBootStrapper()
        {
            var modules = ObjectContainer.ResolveServices<IBootStrapperTask>();
            modules.ForEach(m =>
            {
                m.Execute(BootStrapperTaskArgs.Empty);
            });
        }
    }
}