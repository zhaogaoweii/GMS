using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using ZGW.GMS.Core.Mvc.Routes;

namespace ZGW.GMS.Core.Modules
{
    /// <summary>
    /// 模块基类
    /// </summary>
    public abstract class ModuleBase : IModule
    {
        private readonly RouteCollection routes;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ModuleBase()
        {
            routes = ObjectContainer.ResolveService<RouteCollection>();
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public abstract string ModuleName { get; }

        /// <summary>
        /// 模块初始化
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// 注册路由
        /// </summary>
        public virtual void RegisterRoutes() { }

        /// <summary>
        /// 注册过滤器
        /// </summary>
        /// <param name="filters">过滤器</param>
        public virtual void RegisterFilters(GlobalFilterCollection filters) { }

        /// <summary>
        /// 注册ModelBinders
        /// </summary>
        /// <param name="modelBinders">模型绑定</param>
        public virtual void RegisterModelBinders(ModelBinderDictionary modelBinders) { }

        /// <summary>
        /// 卸载模块
        /// </summary>
        public virtual void Unload() { }

        /// <summary>
        /// 路由集合
        /// </summary>
        protected RouteCollection Routes
        {
            get { return routes; }
        }

        #region MapRoute
        /// <summary>
        /// MapRoute
        /// </summary>
        /// <param name="name">路由名称</param>
        /// <param name="url">路由地址</param>
        protected void MapRoute(string name, string url)
        {
            routes.MapRoute(name, url, null, null, null, ModuleName);
        }

        /// <summary>
        /// MapRoute
        /// </summary>
        /// <param name="name">路由名称</param>
        /// <param name="url">路由地址</param>
        /// <param name="defaults">默认参数</param>
        protected void MapRoute(string name, string url, object defaults)
        {
            routes.MapRoute(name, url, defaults, null, null, ModuleName);
        }

        /// <summary>
        /// MapRoute
        /// </summary>
        /// <param name="name">路由名称</param>
        /// <param name="url">路由地址</param>
        /// <param name="namespaces">命名空间</param>
        protected void MapRoute(string name, string url, string[] namespaces)
        {
            routes.MapRoute(name, url, null, null, namespaces, ModuleName);
        }

        /// <summary>
        /// MapRoute
        /// </summary>
        /// <param name="name">路由名称</param>
        /// <param name="url">路由地址</param>
        /// <param name="defaults">默认参数</param>
        /// <param name="constraints">约束条件</param>
        protected void MapRoute(string name, string url, object defaults, object constraints)
        {
            routes.MapRoute(name, url, defaults, constraints, null, ModuleName);
        }

        /// <summary>
        /// MapRoute
        /// </summary>
        /// <param name="name">路由名称</param>
        /// <param name="url">路由地址</param>
        /// <param name="defaults">默认参数</param>
        /// <param name="namespaces">命名空间</param>
        protected void MapRoute(string name, string url, object defaults, string[] namespaces)
        {
            routes.MapRoute(name, url, defaults, null, namespaces, ModuleName);
        }

        /// <summary>
        /// MapRoute
        /// </summary>
        /// <param name="name">路由名称</param>
        /// <param name="url">路由地址</param>
        /// <param name="defaults">默认参数</param>
        /// <param name="constraints">约束条件</param>
        /// <param name="namespaces">命名空间</param>
        protected void MapRoute(string name, string url, object defaults, object constraints, string[] namespaces)
        {
            routes.MapRoute(name, url, defaults, constraints, namespaces, ModuleName);
        }
        #endregion
    }
}
