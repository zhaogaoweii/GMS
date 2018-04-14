using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using ZGW.GMS.Core.Exceptions;

namespace ZGW.GMS.Core.Mvc.Security
{
    /// <summary>
    /// 防止SQL注入的管道
    /// </summary>
    public class SQLInjectionDefense : IHttpModule
    {
        private const string SQLKeyWord = @"select|insert|delete|from|count|drop|update|execute|exec|declare|truncate|asc|mid|union|char|into|or|and";
        private const string SQLNotation = @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']";

        public void Dispose()
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">HttpApplication</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        private void OnBeginRequest(object sender, EventArgs args)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            Action<NameValueCollection> inspect = delegate(NameValueCollection collection)
            {
                if (collection.Count > 0)
                {
                    for (int i = 0; i < collection.Count; i++)
                    {
                        if (HasInjection(collection[i])) throw new SQLInjectionException("提交的表单包含非法信息!");
                    }
                }
            };

            inspect(context.Request.QueryString);
            inspect(context.Request.Form);
        }

        private bool HasInjection(string keyword){
            return Regex.IsMatch(keyword, SQLKeyWord, RegexOptions.IgnoreCase) 
                || Regex.IsMatch(keyword, SQLNotation);
        }
    }
}
