using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web.Mvc;

namespace ZGW.GMS.Core.Mvc.Routes
{
    /// <summary>
    /// 路由的扩展方法
    /// </summary>
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// 按模块路由
        /// </summary>
        /// <param name="routes">路由集合</param>
        /// <param name="name">名称</param>
        /// <param name="url">地址</param>
        /// <param name="defaults">默认值</param>
        /// <param name="constraints">约束条件</param>
        /// <param name="namespaces">命名空间</param>
        /// <param name="moduleName">模块名称</param>
        /// <returns>路由对象</returns>
        public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces, string moduleName)
        {
            if (url.StartsWith("/"))
                url = url.Substring(1);
            Route route = routes.MapRoute(name, url, defaults, constraints, namespaces);
            if (!SystemHelper.IsNullOrEmpty(moduleName))
                route.DataTokens["area"] = moduleName;
            return route;
        }
    }
}
