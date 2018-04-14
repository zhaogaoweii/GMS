using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 与Decimal类型有关的公共方法
    /// </summary>
    public class DecimalHelper
    {
        /// <summary>
        /// 功能:将指定的参数转成Decimal
        /// 时间：2013-03-25       
        /// </summary>      
        /// <param name="obj">要转的对象</param>
        /// <returns>返回decimal  参数为DBNull、NULL、""  返回 0</returns>
        public static decimal ToDecimal(object obj)
        {
            return (Convert.IsDBNull(obj) || obj == DBNull.Value || obj == null || obj.ToString() == "") ? 0 : Convert.ToDecimal(obj);
        }
    }
}
