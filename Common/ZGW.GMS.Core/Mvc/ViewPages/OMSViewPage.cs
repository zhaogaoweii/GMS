using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZGW.GMS.Core.Localization;

namespace ZGW.GMS.Core.Mvc.ViewPages
{
    /// <summary>
    /// OMS的ViewPage基类
    /// </summary>
    /// <typeparam name="TModel">实体类型</typeparam>
    public abstract class OMSViewPage<TModel> : WebViewPage<TModel>
    {
        /// <summary>
        /// 本地化委托
        /// </summary>
        public Localizer T
        {
            get
            {
                return (text, args) =>
                {
                    if (args != null && args.Length > 0)
                    {
                        text = String.Format(text, args);
                    }
                    return new MvcHtmlString(text);
                };
            }
        }
    }
}
