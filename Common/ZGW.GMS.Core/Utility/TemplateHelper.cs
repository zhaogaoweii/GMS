using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core.Exceptions;


namespace ZGW.GMS.Core
{
    ///// <summary>
    ///// 模板辅助类
    ///// </summary>
    //public static class TemplateHelper
    //{
    //    /// <summary>
    //    /// 获取模板数据
    //    /// </summary>
    //    /// <param name="tpl">模板字符串</param>
    //    /// <param name="data">业务数据</param>
    //    /// <returns>转化后的字符串</returns>
    //    public static string EvaluateTemplate(string tpl, Dictionary<string, object> data)
    //    {
    //        foreach (var item in data)
    //        {
    //            tpl = tpl.Replace(String.Format("${{{0}}}", item.Key), (item.Value??"").ToString());
    //        }
    //        return tpl;
    //    }
    //}
}
