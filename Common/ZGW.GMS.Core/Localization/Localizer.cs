using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZGW.GMS.Core.Localization
{
    /// <summary>
    /// 本地化处理的委托
    /// </summary>
    /// <param name="text">关键字</param>
    /// <param name="args">传入的参数</param>
    /// <returns>本地化后的显示数据</returns>
    public delegate MvcHtmlString Localizer(string text,params object[] args);
}
