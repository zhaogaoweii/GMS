using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Wcf;
using ZGW.GMS.Core.Data;
using ZGW.GMS.Core.Tasks;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 服务端容器注入
    /// </summary>
    public class ServiceRegistration : Autofac.Module
    {
        private readonly Func<Assembly[]> assebmlyLoader;

        public ServiceRegistration()
            : this(() => SystemHelper.LoadAppAssemblies())
        {

        }

        public ServiceRegistration(Func<Assembly[]> assebmlyLoader)
        {
            this.assebmlyLoader = assebmlyLoader;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //注册无效的类型
            RegisterInvalidTypes();

            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            //builder.RegisterGeneric(typeof(RepositoryHistory<>)).As(typeof(IRepositoryHistory<>));
            
            RegisterAssemblies(builder);

            //注册工作单元
            builder.RegisterUnitOfWork(assebmlyLoader);
        }

        /// <summary>
        /// 注册Service,BusinessLogic,Repository
        /// </summary>
        /// <param name="builder"></param>
        private void RegisterAssemblies(ContainerBuilder builder)
        {
            Assembly[] assemblies = assebmlyLoader();

            builder.RegisterAssemblyTypesWithLiftTimeScope(assemblies).AsImplementedInterfaces().AsSelf();
        }

        ///// <summary>
        ///// 注册Hibernate的映射文件
        ///// </summary>
        ///// <param name="config"></param>
        //private void RegisterMappingAssemblies(NHibernate.Cfg.Configuration config)
        //{
        //    var mappingAssemblies = assebmlyLoader();
        //    foreach (var assembly in mappingAssemblies)
        //    {
        //        config.AddAssembly(assembly);
        //    }
        //}

        /// <summary>
        /// 注册无效的类型
        /// </summary>
        private void RegisterInvalidTypes()
        {
            InvalidTypes.Instance.Register<LoadModuleTask>();
        }
    }
}
