using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 与日期有关的公共方法
    /// </summary>
    public class DateTimeHelper 
    {
        /// <summary>
        /// 功能：跟据身份证号取得出生日期
        /// 日期：2013-04-01        
        /// </summary>
        /// <param name="card_code">身份证号码</param>
        /// <returns>出生日期</returns>
        public static string GetBirthDayByCardCode(string card_code)
        {
            if (string.IsNullOrEmpty(card_code))
            {
                return null;
            }
            if (card_code.Length == 15)
            {
                return ("19" + card_code.Substring(6, 2)) + "-" + card_code.Substring(8, 2) + "-" + card_code.Substring(10, 2);
            }
            if (card_code.Length == 18)
            {
                return (card_code.Substring(6, 4)) + "-" + card_code.Substring(10, 2) + "-" + card_code.Substring(12, 2);
            }
            return null;
        }

        /// <summary>
        /// 功能：将日期对像转换成字符串格式（yyyy-MM-dd）
        /// 时间：2013-03-25        
        /// </summary>   
        /// <param name="obj">要转的对象</param>
        /// <returns>yyyy-MM-dd 格式的日期</returns>
        public static string DateFormatToShort(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == DBNull.Value || obj == null || String.IsNullOrEmpty(obj.ToString()))
            {
                return "";
            }
            return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 功能：将日期对像转换成字符串格式（yyyy-MM-dd HH:mm:ss）
        /// 时间：2013-03-25        
        /// </summary>       
        /// <param name="obj">要转的对象</param>
        /// <returns>yyyy-MM-dd HH:mm:ss 格式的日期</returns>
        public static string DateFormatToLong(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == DBNull.Value || obj == null || String.IsNullOrEmpty(obj.ToString()))
            {
                return "";
            }
            return Convert.ToDateTime(obj).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 功能：将字符串转换成Oracel日期格式（TO_DDATE('','YYYY-MM-DD HH24:MI:SS')）
        /// 时间：2013-03-25        
        /// </summary>
        /// <param name="obj">要转的对象</param>
        /// <returns>TO_DDATE('obj','YYYY-MM-DD HH24:MI:SS')</returns>
        public static string OracelDateFormat(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == DBNull.Value || obj == null || string.IsNullOrEmpty(obj.ToString()))
            {
                return "NULL";
            }
            return "TO_DATE('" + Convert.ToDateTime(obj).ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS')";
        }

        /// <summary>
        /// 功能:取得当前时间(yyyy-MM-dd HH:mm:ss)        
        /// 时间:2012-7-19
        /// </summary>
        /// <returns>当前时间(yyyy-MM-dd HH:mm:ss)</returns>
        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 功能:取得当前时间(yyyy-MM-dd)
        /// 时间:2012-7-19
        /// </summary>
        /// <returns>当前时间(yyyy-MM-dd)</returns>
        public static string GetCurrentShorDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 功能:取得某月的第一天        
        /// 时间:2013-03-25
        /// </summary>
        /// <param name="obj">日期的对象</param>
        /// <returns>月份的第一天（yyyy-MM-01）</returns>
        public static string GetMonthFirstDay(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == DBNull.Value || obj == null || string.IsNullOrEmpty(obj.ToString()))
            {
                return "";
            }
            return Convert.ToDateTime(obj).ToString("yyyy-MM-01");
        }

        /// <summary>
        /// 功能:取得某月的最后一天        
        /// 时间:2013-03-25
        /// </summary>
        /// <param name="obj">要转的对象</param>
        /// <returns>日期（yyyy-MM-28||29||30||31）</returns>
        public static string GetMonthLastDay(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == DBNull.Value || obj == null || string.IsNullOrEmpty(obj.ToString()))
            {
                return "";
            }
            DateTime dt = Convert.ToDateTime(obj);
            int year = dt.Year;
            int month = dt.Month;
            int day = DateTime.DaysInMonth(year, month);
            return Convert.ToDateTime(year.ToString() + '-' + month.ToString() + '-' + day.ToString()).ToString("yyyy-MM-dd");
        }       

        /// <summary>
        /// 功能：返回日期区间的sql
        /// </summary>
        /// <param name="colName">列名</param>
        /// <param name="dateFrom">开始时间</param>
        /// <param name="dateTo">结束时间</param>
        /// <returns>返回 Date BETWEEN d1 and d2</returns>
        public static string GetDateBetweenSQL(string colName, string dateFrom, string dateTo)
        {
            if (string.IsNullOrEmpty(dateFrom))
            {
                if (string.IsNullOrEmpty(dateTo))
                {
                    return " ";
                }
                else
                {
                    return " AND " + colName + " <= " + DateTimeHelper.OracelDateFormat(dateTo) + " ";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(dateTo))
                {
                    return " AND " + colName + " >= " + DateTimeHelper.OracelDateFormat(dateFrom) + " ";
                }
                else
                {
                    return " AND " + colName + " between " + DateTimeHelper.OracelDateFormat(dateFrom) + " and " + DateTimeHelper.OracelDateFormat(dateTo) + " ";
                }
            }
        }

        /// <summary>
        /// 功能:智翼通日期与OMS日期比较        
        /// 日期2012-5-26
        /// </summary>    
        /// <param name="obj_1">智翼通日期</param>
        /// <param name="obj_2">OMS日期</param>
        /// <returns>返回Boolean 相等返回True 不相等返回False</returns> 
        public static bool ZYTAndOMSDateCompare(object obj_1, object obj_2)
        {
            string str_1 = "";
            if (obj_1.GetType() == typeof(decimal))
            {
                str_1 = Convert.IsDBNull(obj_1) || string.IsNullOrEmpty(obj_1.ToString()) ? "" : Convert.ToDateTime(obj_1.ToString() + ".1").ToString("yyyy-MM-dd");
            }
            else
            {
                str_1 = Convert.IsDBNull(obj_1) || string.IsNullOrEmpty(obj_1.ToString()) ? "" : Convert.ToDateTime(obj_1).ToString("yyyy-MM-dd");
            }
            string str_2 = Convert.IsDBNull(obj_2) || string.IsNullOrEmpty(obj_2.ToString()) ? "" : Convert.ToDateTime(obj_2).ToString("yyyy-MM-dd");
            return str_1 == str_2 ? true : false;
        }

        /// <summary>
        /// 功能：判断对象是否是日期
        /// 日期：2013-04-01        
        /// </summary>
        /// <param name="obj">要判断的对象</param>
        /// <returns>是True 否 False</returns>
        public static bool CheckIsDateTime(object obj)
        {
            return obj.GetType() == typeof(DateTime);
        }
        
    }
}
