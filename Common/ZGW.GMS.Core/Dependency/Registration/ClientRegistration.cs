using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.Wcf;
using ZGW.GMS.Core.Caching;
using ZGW.GMS.Core.Mvc.ModelBinders;

using System.Web.Mvc;
using ZGW.GMS.Core.DBLogging.Impl;
using System.Web.Routing;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 客服端容器注入
    /// </summary>
    public class ClientRegistration:Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            try
            {
                //注册无效的类型
                RegisterInvalidTypes();

                Assembly[] assemblies = SystemHelper.LoadAppAssemblies();
                builder.RegisterAssemblyTypesWithLiftTimeScope(assemblies).AsImplementedInterfaces().AsSelf();

                //注册WCF的契约
                 //builder.RegisterWCFContract(assemblies, m => m.Location.EndsWith(".Service.Interface.dll", StringComparison.CurrentCultureIgnoreCase));

                //注册ModelBinders
                builder.RegisterModelBinders(typeof(Int32ListModelBinder).Assembly);
                builder.RegisterModelBinderProvider();
                //注册Controllers
                builder.RegisterControllers(assemblies, m => m.Location.EndsWith(".Web.dll", StringComparison.CurrentCultureIgnoreCase));
                //注册路由
                builder.RegisterInstance(RouteTable.Routes).As<RouteCollection>();
                //注册GlobalFilters
                builder.RegisterInstance(GlobalFilters.Filters).As<GlobalFilterCollection>();
                //注册ModelBinders
                builder.RegisterInstance(ModelBinders.Binders);
                #region
                ////注册缓存
                //builder.RegisterType<DefaultCacheProvider>().Named<ICacheProvider>("aspnet").AsSelf().SingleInstance();
                //builder.RegisterType<AppFabricCacheProvider>().Named<ICacheProvider>("appfabric").AsSelf().SingleInstance();

                ////注册日志
                //builder.RegisterLogger();
                #endregion
            }
            catch (Exception ex)
            {
                
                throw;
            }
           
        }

        private void RegisterInvalidTypes()
        {
           // InvalidTypes.Instance.Register<DBLogBackgroundTask>();
        }
    }
}
