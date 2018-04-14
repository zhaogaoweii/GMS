using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ZGW.GMS.Core.Mvc.ModelBinders
{
    /// <summary>
    /// 解析逗号分隔的Int32列表
    /// </summary>
    public class Int32ListModelBinder : IModelBinder
    {
        /// <summary>
        /// 使用指定的控制器上下文和绑定上下文将模型绑定到一个值。
        /// </summary>
        /// <param name="controllerContext">控制器上下文</param>
        /// <param name="bindingContext">绑定上下文</param>
        /// <returns>绑定值</returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != null)
            {
                string[] array = valueProviderResult.RawValue as string[];
                if (array != null)
                {
                    int tryInt = 0;
                    return (from i in array
                            where Int32.TryParse(i, out tryInt)
                            select tryInt).ToList();
                }
            }

            return new List<Int32>();
        }
    }
}
