using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ZGW.GMS.Core.Mvc.ModelBinders
{
    //public class JsonBinder<T> : IModelBinder
    //{
        //public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    //从请求中获取提交的参数数据
        //    string json = bindingContext.ValueProvider.GetAttemptedValue(bindingContext.ModelName);
        //    if (json.IsNullOrWhiteSpace())
        //    {
        //        return null;
        //    }
        //    JsonSerializer js = new JsonSerializer();
        //    //提交参数是对象
        //    if (json.StartsWith("{") && json.EndsWith("}"))
        //    {
        //        JObject jsonBody = JObject.Parse(json);
        //        object obj = js.Deserialize(jsonBody.CreateReader(), typeof(T));
        //        return obj;
        //    }
        //    //提交参数是数组
        //    if (json.StartsWith("[") && json.EndsWith("]"))
        //    {
        //        IList<T> list = new List<T>();
        //        JArray jsonRsp = JArray.Parse(json);
        //        if (jsonRsp != null)
        //        {
        //            for (int i = 0; i < jsonRsp.Count; i++)
        //            {
        //                object obj = js.Deserialize(jsonRsp[i].CreateReader(), typeof(T));
        //                list.Add((T)obj);
        //            }
        //        }
        //        return list;
        //    }
        //    return null;
        //}
    //}
}
