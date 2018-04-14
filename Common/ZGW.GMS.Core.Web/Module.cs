using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core;
using ZGW.GMS.Core.Modules;


namespace ZGW.GMS.Core
{
    /// <summary>
    /// 站点的模块注册信息
    /// </summary>
    [ComponentRegistry]
    public class Module : ModuleBase
    {
        /// <summary>
        /// 模型名称
        /// </summary>
        public override string ModuleName
        {
            get { return "Core.Web"; }
        }

        /// <summary>
        /// 注册模型绑定
        /// </summary>
        /// <param name="modelBinders">ModelBinderDictionary对象</param>
        //public override void RegisterModelBinders(System.Web.Mvc.ModelBinderDictionary modelBinders)
        //{
        //   modelBinders.Add(new KeyValuePair<Type, System.Web.Mvc.IModelBinder>(typeof(AttachmentInfo), new AttachmentModelBinder()));
        //}
    }
}
