using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZGW.GMS.Core.Mvc
{
    /// <summary>
    /// 所有Controller的基类
    /// </summary>
    public class BaseController:Controller
    {
        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 格式的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">转化为Json的对象</param>
        /// <param name="contentType">Content Type</param>
        /// <param name="contentEncoding">Content Encoding</param>
        /// <returns>JsonResult</returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            var result = new NewtonJsonResult();
            result.ContentType = contentType;
            result.ContentEncoding = contentEncoding;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
