using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZGW.GMS.Core.Modules
{
    /// <summary>
    /// 模块配置
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// 初始化模块
        /// </summary>
        void Initialize();

        /// <summary>
        /// 注册路由配置
        /// </summary>
        void RegisterRoutes();

        /// <summary>
        /// 注册过滤器
        /// </summary>
        /// <param name="filters">全局过滤器</param>
        void RegisterFilters(GlobalFilterCollection filters);

        /// <summary>
        /// 注册模型绑定器
        /// </summary>
        /// <param name="modelBinders">模型绑定器</param>
        void RegisterModelBinders(ModelBinderDictionary modelBinders);

        /// <summary>
        /// 卸载模块
        /// </summary>
        void Unload();
    }
}
