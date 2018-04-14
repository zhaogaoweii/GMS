using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using ZGW.GMS.Core.Exceptions;

namespace ZGW.GMS.Core.Mvc.Filters
{
    /// <summary>
    /// 对已做Base64编码的Action参数ID进行解码
    /// </summary>
    public class IDEncodingAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// ID数据类型
        /// </summary>
        public Type IDType { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IDEncodingAttribute()
            :this(typeof(int))
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="idType">Id类型</param>
        public IDEncodingAttribute(Type idType)
        {
            IDType = idType;
        }

        /// <summary>
        /// Action执行之前的Filter方法
        /// </summary>
        /// <param name="filterContext">ActionExecutingContext</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.RouteData.Values.ContainsKey("id"))
            {
                throw new OMSException("路由中不包含ID。");
            }

            try
            {
                var encodedID = filterContext.RouteData.Values["id"] as string;
                filterContext.ActionParameters["id"] = Convert.ChangeType(encodedID.FromBase64(), IDType);
            }
            catch (FormatException exception)
            {
                throw new OMSException("ID为非法字符。", exception);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
