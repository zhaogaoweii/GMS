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
    /// 模块相关的注册
    /// </summary>
    [ComponentRegistry]
    public class CoreModule : ModuleBase
    {
        private readonly IModuleService moduleService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="moduleService">模块服务接口</param>
        public CoreModule(IModuleService moduleService)
        {
            this.moduleService = moduleService;
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public override string ModuleName
        {
            get { return "Module.Core"; }
        }

        /// <summary>
        /// 注册路由
        /// </summary>
        public override void RegisterRoutes()
        {
            var modules = moduleService.LoadAllModuleInfo().OrderBy(m => m.Order);
            foreach (var item in modules.SelectMany(m => m.Routes))
            {
                RouteValueDictionary defaults = new RouteValueDictionary();
                defaults.Add("controller", item.Controller);
                defaults.Add("action", item.Action);
                foreach (var attr in item.Attributes)
                {
                    defaults.Add(attr.Key, GetDefaultValue(attr.Value));
                }
                Routes.MapRoute(item.Name, item.Url, defaults, null, item.Namespaces, item.Module);
            }
        }

        private object GetDefaultValue(string val)
        {
            object result = val;

            if (StringHelper.IsEqual(val, "[Optional]", true))
            {
                result = UrlParameter.Optional;
            }
            return result;
        }
    }
}
