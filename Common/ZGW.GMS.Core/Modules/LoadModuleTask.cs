using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZGW.GMS.Core.Modules;

namespace ZGW.GMS.Core.Tasks
{
    /// <summary>
    /// 加载模块的任务
    /// </summary>
    [ComponentRegistry]
    [TaskUsageAttribute(HostTargets.Web)]
    public class LoadModuleTask:IBootStrapperTask
    {
        private readonly IEnumerable<IModule> modules;
        private readonly GlobalFilterCollection filters;
        private readonly ModelBinderDictionary modelBinders;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="modules">系统中所有的模块</param>
        /// <param name="filters">过滤器</param>
        /// <param name="modelBinders">模型绑定</param>
        public LoadModuleTask(IEnumerable<IModule> modules,
            GlobalFilterCollection filters,
            ModelBinderDictionary modelBinders)
        {
            this.modules=modules;
            this.filters = filters;
            this.modelBinders = modelBinders;
        }

        /// <summary>
        /// 模块初始化
        /// </summary>
        /// <param name="args">起动参数</param>
        public void Execute(BootStrapperTaskArgs args)
        {
            foreach (var item in modules)
            {
                item.Initialize();
                item.RegisterFilters(filters);
                item.RegisterModelBinders(modelBinders);
                item.RegisterRoutes();
            }
        }

        /// <summary>
        /// 卸载模块
        /// </summary>
        /// <param name="args">启动参数</param>
        public void Cleanup(BootStrapperTaskArgs args)
        {
            foreach (var item in modules)
            {
                item.Unload();
            }
        }
    }
}
