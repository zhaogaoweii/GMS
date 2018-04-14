using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.ServiceModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Autofac;
using Autofac.Core;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Autofac.Integration.Wcf;
using ZGW.GMS.Core.Modules;
using ZGW.GMS.Core.Logging;

using ZGW.GMS.Core.Data;


namespace ZGW.GMS.Core
{
    /// <summary>
    /// DI扩展方法
    /// </summary>
    public static class RegisterExtentions
    {

        /// <summary>
        /// 根据组件的生命周期定义注册组件
        /// </summary>
        /// <param name="buidler">Autofac的IContainer对象</param>
        /// <param name="assemblies">注册的程序集</param>
        /// <returns>Autofac的注册构造器枚举</returns>
        public static IEnumerable<IRegistrationBuilder<object, Autofac.Features.Scanning.ScanningActivatorData, DynamicRegistrationStyle>> RegisterAssemblyTypesWithLiftTimeScope(this ContainerBuilder buidler, params Assembly[] assemblies)
        {
            IEnumerable<IRegistrationBuilder<object, Autofac.Features.Scanning.ScanningActivatorData, DynamicRegistrationStyle>> builders =
                new IRegistrationBuilder<object, Autofac.Features.Scanning.ScanningActivatorData, DynamicRegistrationStyle>[]{
                    //瞬态
                    buidler.RegisterAssemblyTypes(assemblies).Where(m =>ComponentRegistryAttribute.ValidateType(m,Lifetime.Transient)).InstancePerDependency(),
                    //单例
                    buidler.RegisterAssemblyTypes(assemblies).Where(m =>ComponentRegistryAttribute.ValidateType(m,Lifetime.Singleton)).SingleInstance(),
                    //容器
                    buidler.RegisterAssemblyTypes(assemblies).Where(m =>ComponentRegistryAttribute.ValidateType(m,Lifetime.Container)).InstancePerLifetimeScope(),
                };
            return builders;
        }

        /// <summary>
        /// 按接口注册组件服务
        /// </summary>
        /// <param name="builders">Autofac的IContainer对象</param>
        /// <returns>Autofac的注册构造器枚举</returns>
        public static IEnumerable<IRegistrationBuilder<object, Autofac.Features.Scanning.ScanningActivatorData, DynamicRegistrationStyle>> AsImplementedInterfaces(this IEnumerable<IRegistrationBuilder<object, Autofac.Features.Scanning.ScanningActivatorData, DynamicRegistrationStyle>> builders)
        {
            if (builders == null)
            {
                throw new ArgumentNullException("builders为空");
            }

            builders.ForEach(m => m.AsImplementedInterfaces());
            return builders;
        }

        /// <summary>
        /// 注册成自身实例
        /// </summary>
        /// <param name="builders">Autofac的IContainer对象</param>
        /// <returns>Autofac的注册构造器枚举</returns>
        public static IEnumerable<IRegistrationBuilder<object, Autofac.Features.Scanning.ScanningActivatorData, DynamicRegistrationStyle>> AsSelf(this IEnumerable<IRegistrationBuilder<object, Autofac.Features.Scanning.ScanningActivatorData, DynamicRegistrationStyle>> builders)
        {
            if (builders == null)
            {
                throw new ArgumentNullException("builders为空");
            }

            builders.ForEach(m => m.AsSelf());
            return builders;
        }

        /// <summary>
        /// 注册Controllers
        /// </summary>
        /// <param name="builder">Autofac的IContainer对象</param>
        /// <param name="assemblies">注册的程序集</param>
        /// <param name="filter">过滤的Lambada表达示</param>
        public static void RegisterControllers(this ContainerBuilder builder, IEnumerable<Assembly> assemblies, Func<Assembly, bool> filter)
        {
            if (assemblies != null && filter != null)
            {
                assemblies = assemblies.Where(filter);
            }

            builder.RegisterControllers(assemblies.ToArray());
        }

        /// <summary>
        /// 在客户端注册WCF的契约接口
        /// </summary>
        /// <param name="builder">Autofac的IContainer对象</param>
        /// <param name="assemblies">注册的程序集</param>
        /// <param name="filter">过滤的Lambada表达示</param>
        public static void RegisterWCFContract(this ContainerBuilder builder, IEnumerable<Assembly> assemblies, Func<Assembly, bool> filter)
        {
            if (assemblies != null && filter != null)
            {
                assemblies = assemblies.Where(filter);
            }

            foreach (var assembly in assemblies)
            {
                var serviceTypes = assembly.GetTypes().Where(m => m.IsDefined(typeof(ServiceContractAttribute)));
                foreach (Type serviceType in serviceTypes)
                {
                    if (serviceType.Name.ToUpper() != "IZYTBEIJINGINTERFACE_TEST")
                    {
                        Type channelFactoryType = typeof(ChannelFactory<>).MakeGenericType(serviceType);
                        builder.RegisterInstance(Activator.CreateInstance(channelFactoryType, new object[] { serviceType.FullName })).As(channelFactoryType).SingleInstance();

                        builder.Register(m =>
                        {
                            dynamic instance = m.Resolve(channelFactoryType);
                            return instance.CreateChannel();
                        }).As(serviceType).UseWcfSafeRelease();
                    }
                }
            }
        }

        /// <summary>
        /// 在客户端注册模块
        /// </summary>
        /// <param name="builder">Autofac的IContainer对象</param>
        /// <param name="assemblies">Autofac的IContainer对象</param>
        /// <param name="filter">过滤的Lambada表达示</param>
        public static void RegisterModules(this ContainerBuilder builder, IEnumerable<Assembly> assemblies, Func<Assembly, bool> filter = null)
        {
            if (assemblies != null && filter != null)
            {
                assemblies = assemblies.Where(filter);
            }

            foreach (var assembly in assemblies)
            {
                var moduleTypes = assembly.GetTypes().Where(m => typeof(ZGW.GMS.Core.Modules.IModule).IsAssignableFrom(m) && !m.IsAbstract);
                foreach (var type in moduleTypes)
                {
                    builder.RegisterType(type).As(typeof(ZGW.GMS.Core.Modules.IModule));
                }
            }
        }

        /// <summary>
        /// 注册工作单元
        /// </summary>
        /// <param name="builder">Autofac的IContainer对象</param>
        public static void RegisterUnitOfWork(this ContainerBuilder builder, Func<Assembly[]> assemblyLoader)
        {
            //builder.Register((c, p) =>
            //{
            //    DBCatetory dbCategory = p.Any() ? p.Named<DBCatetory>("DBCategory") : DBCatetory.Production;
            //    ISessionFactory sessionFactory = NHSessionFactoryProvider.Instance.GetSessionFactory(assemblyLoader,dbCategory);
            //    return new UnitOfWork(sessionFactory);
            //}).As<IUnitOfWork>();
        }
    }
}
