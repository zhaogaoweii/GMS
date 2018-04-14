using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using ZGW.GMS.Core.Mvc.ModelBinders;

namespace ZGW.GMS.Core.Modules
{
    /// <summary>
    /// MVC的模块定义
    /// </summary>
    [ComponentRegistry]
    public class MvcModule:ModuleBase
    {
        /// <summary>
        /// 模型名称
        /// </summary>
        public override string ModuleName
        {
            get { return "MvcModule"; }
        }

        /// <summary>
        /// 注册路由
        /// </summary>
        public override void RegisterRoutes()
        {
            Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            Routes.IgnoreRoute("{resource}.aspx");
        }

        /// <summary>
        /// 注册过滤器
        /// </summary>
        /// <param name="filters">过滤器集合</param>
        public override void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// 注册模型绑定
        /// </summary>
        /// <param name="modelBinders">模型绑定器</param>
        public override void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders.Add(new KeyValuePair<Type, IModelBinder>(typeof(int[]), new Int32ArrayModelBinder()));
        }
    }
}
