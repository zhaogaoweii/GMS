using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZGW.GMS.Core.Mvc.ModelBinders
{
     /// <summary>
    /// 解析逗号分隔的Int32列表
    /// </summary>
    public class Int32ArrayModelBinder : IModelBinder
    {
        /// <summary>
        /// 使用指定的控制器上下文和绑定上下文将模型绑定到一个值。
        /// </summary>
        /// <param name="controllerContext">控制器上下文</param>
        /// <param name="bindingContext">绑定上下文</param>
        /// <returns>绑定值</returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string key = bindingContext.ModelName + "[]";
            var valueProviderResult = bindingContext.ValueProvider.GetValue(key);
            if (valueProviderResult != null)
            {
                string[] array = valueProviderResult.RawValue as string[];
                return ConvertStrArrayToIntArray(array);
            }
            else
            {
                key = bindingContext.ModelName;
                valueProviderResult = bindingContext.ValueProvider.GetValue(key);

                if (valueProviderResult != null )
                {
                    string[] array = valueProviderResult.RawValue as string[];
                    if (array != null && array.Length >0)
                    {
                        array = array[0].Split(',');
                        return ConvertStrArrayToIntArray(array);
                    }
                }
            }

            return new int[0];
        }

        private int[] ConvertStrArrayToIntArray(string[] strArray)
        {
            if (strArray != null)
            {
                int tryInt = 0;
                return (from i in strArray
                        where Int32.TryParse(i, out tryInt)
                        select tryInt).ToArray();
            }
            return null;
        }
    }
}
