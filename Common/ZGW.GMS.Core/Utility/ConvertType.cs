using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Utility
{
    public static class ConvertType
    {
        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultVal">The default value.</param>
        /// <returns></returns>
        public static Int32 ToInt32(this Object obj, int defaultVal = 0)
        {
            if (obj == null)
            {
                return defaultVal;
            }

            int result = 0;
            if (int.TryParse(obj.ToString(), out result))
            {
                return result;
            }

            return defaultVal;
        }

        /// <summary>
        /// 如果转换失败, 则返回 DateTime.MinValue
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this Object obj)
        {
            var datetime = DateTime.MinValue;
            if (obj == null)
            {
                return datetime;
            }

            if (DateTime.TryParse(obj.ToString(), out datetime))
            {
                return datetime;
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// 如果转换失败, 则返回 null
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(this Object obj)
        {
            var datetime = DateTime.MinValue;
            if (obj == null)
            {
                return null;
            }

            if (DateTime.TryParse(obj.ToString(), out datetime))
            {
                return datetime;
            }
            return null;
        }
    }
}
