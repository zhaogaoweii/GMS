using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZGW.GMS.Core.Mvc
{
    /// <summary>
    /// 表示一个类，该类用NewtonSoft将 JSON 格式的内容发送到响应。
    /// </summary>
    public class NewtonJsonResult:JsonResult
    {
        /// <summary>
        /// 通过从 System.Web.Mvc.ActionResult 类继承的自定义类型，启用对操作方法结果的处理。
        /// </summary>
        /// <param name="context">执行结果时所处的上下文。</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!ContentType.IsNullOrEmpty())
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                response.Write(JsonConvert.SerializeObject(Data));
            }
        }
    }

    /// <summary>
    /// 表示一个类，该类用NewtonSoft将 JSON 格式的内容发送到响应。
    /// </summary>
    public class NewtonJsonResultWithDateTime : JsonResult
    {
        /// <summary>
        /// 通过从 System.Web.Mvc.ActionResult 类继承的自定义类型，启用对操作方法结果的处理。
        /// </summary>
        /// <param name="context">执行结果时所处的上下文。</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!ContentType.IsNullOrEmpty())
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }


            if (Data != null)
            {
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd";
                response.Write(JsonConvert.SerializeObject(base.Data, Newtonsoft.Json.Formatting.Indented, timeFormat));
            }
        }
    }

}
