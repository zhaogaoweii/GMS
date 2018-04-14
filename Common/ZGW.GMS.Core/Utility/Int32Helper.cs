using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 与Int32 有关的公共方法
    /// </summary>
    public class Int32Helper
    {
        /// <summary>
        /// 功能:将指定的参数转成ToInt32
        /// 时间：2013-03-25        
        /// </summary>  
        /// <param name="obj">要转的对象</param>
        /// <returns>返回ToInt32  参数为DBNull、NULL、""  返回 0</returns>
        public static int ToInt32(object obj)
        {
            return (Convert.IsDBNull(obj) || obj == DBNull.Value || obj == null || obj.ToString() == "") ? 0 : Convert.ToInt32(obj);
        }        
    }
}
