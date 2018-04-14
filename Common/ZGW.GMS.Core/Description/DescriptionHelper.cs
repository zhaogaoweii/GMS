/// <summary>
/// 作者：刘宇华
/// 创建时间：2013/7/18 13:47:34
/// 修改人：Administrator
/// 修改时间：
/// 修改备注：
/// 版本：V1.0
/// </summary> 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Description
{
    /// <summary>
    /// DescriptionHelper
    /// </summary>
    public class DescriptionHelper
    {
        /// <summary>
        /// 获取枚举类型值的description
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="enumValue">枚举的值（枚举的名称或数字值)</param>
        /// <returns></returns>
        public static string GetEnumDescription(Type enumType,string enumValue)
        {
            object enumData = Enum.Parse(enumType, enumValue);
            DescriptionAttribute dna = null;
            FieldInfo field = enumType.GetField(Enum.GetName(enumType, enumData));
            dna = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
            {
                return dna.Description;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取枚举类型值的description
        /// </summary>
        /// <param name="enumData">枚举对象</param>
        /// <returns></returns>
        public static string GetEnumDescription(object enumData)
        {
            Type enumType = enumData.GetType();
            return GetEnumDescription(enumType, enumData.ToString());
        }
    }
}
