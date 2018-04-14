using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 实例对象容器
    /// </summary>
    public class ObjectContainer
    {
        private static IContainer container;

        /// <summary>
        /// 注册Autofac容器
        /// </summary>
        /// <param name="objectContainer">Autofac容器</param>
        public static void SetContainer(IContainer objectContainer)
        {
            container = objectContainer;
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>对象实例</returns>
        public static T ResolveService<T>()
        {
            if (container == null)
                return default(T);

            return container.Resolve<T>();
        }

        /// <summary>
        /// 根据参数解析对象
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="parameters">解析实例时用到的参数</param>
        /// <returns>对象实例</returns>
        public static T ResolveService<T>(IDictionary<string, object> parameters)
        {
            if (container == null)
                return default(T);

            var args = parameters.Select(m => new NamedParameter(m.Key, m.Value)).ToArray();

            return container.Resolve<T>(args);
        }

        /// <summary>
        /// 根据Name解析对象
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="name">实例的Name</param>
        /// <returns>对象实例</returns>
        public static T ResolveNamed<T>(string name)
        {
            if (container == null)
                return default(T);

            return container.ResolveNamed<T>(name);
        }

        /// <summary>
        /// 根据对象类型解析对象
        /// </summary>
        /// <param name="instanceType">实例类型</param>
        /// <returns>对象实例</returns>
        public static object ResolveService(Type instanceType)
        {
            if (container == null)
                return null;

            return container.Resolve(instanceType);
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>对象实例集和</returns>
        public static IEnumerable<T> ResolveServices<T>()
        {
            return ResolveService<IEnumerable<T>>();
        }
    }
}
