using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 数据验证
    /// </summary>
    public class ValidationHelper
    {        
        /// <summary>
        /// 功能：判断DataSet中是否有数据
        /// 时间：2013-03-25        
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <returns>返回Boolean 有数据返回True 没有数据返回False</returns>
        public static bool CheckDs(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 功能：判断DataRow[]中是否有数据
        /// 时间：2013-03-25        
        /// </summary> 
        /// <param name="rows">数据源</param>
        /// <returns>返回Boolean 有数据返回True 没有数据返回False</returns>
        public static bool CheckRows(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 功能：判断DataTable中是否有数据
        /// 时间：2013-03-25        
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <returns>返回Boolean 有数据返回True 没有数据返回False</returns>
        public static bool CheckDt(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
        /// <summary>
        /// 功能：判断是否数字(包含小数)
        /// 日期：2013-03-25        
        /// </summary>      
        /// <param name="strNumber">参数 字符串</param>
        /// <returns>返回Boolean 是数字返回True 非数字返回False</returns>
        public static bool CheckIsNumber(string strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }


        /// <summary>
        /// 功能：判断是否为正数
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool CheckIsPositiveNumber(string strNumber) 
        {
            Regex rex = new Regex("^((0|[1-9][0-9]*)(\\.[0-9]+)?)?$");
            Match m = rex.Match(strNumber);
            if (m.Success)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        /// <summary>
        /// 功能：验证是否大于0小于1
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool CheckIsDecimals(string strNumber,bool isNull=false) 
        {
            //判断传入空是否允许通过
            if (string.IsNullOrEmpty(strNumber) && isNull) 
            {
                return true;
            }
            Regex rex = new Regex("^0(\\.[0-9]+)?$");
            Match m = rex.Match(strNumber);
            if (m.Success)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        /// <summary>
        /// 功能：判断是否是整数
        /// 日期：2013-03-25        
        /// </summary>      
        /// <param name="str">参数 字符串</param>
        /// <returns>返回Boolean 是整数返回True 非整数返回False</returns>
        public static bool CheckIsInt(string str)
        {
            int Point_index = str.IndexOf(".", 0, str.Length);
            Decimal tmpd = 0;
            if (Point_index != -1)
            {
                int nLength = str.Length - Point_index - 1;
                string tmp = str.Substring(Point_index + 1, nLength);
                if (tmp == "")
                {
                    //小数点后为空
                    return true;
                }
                tmpd = decimal.Parse(tmp);
                if (tmpd != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 功能:判断是否有小数部分      
        /// 日期2011-11-23
        /// </summary>
        /// <param name="objValue">要判断的对象</param>
        /// <returns>返回Boolean 包含返回True 不包含返回False</returns>
        public static bool CheckIsDouble(object objValue)
        {
            String str = objValue.ToString();
            if (!String.IsNullOrEmpty(str))
            {
                if (str.IndexOf('.') == -1)
                {
                    return false;
                }
                else if (Convert.ToInt32(str.Substring(str.IndexOf(".") + 1)) > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 功能：判断EMAIL地址是否合法
        /// 日期：2013-03-25        
        /// </summary>     
        /// <param name="email">参数 邮箱地址</param>
        /// <returns>返回Boolean 合法返回True 不合法返回False</returns>
        public static bool CheckEmail(string email)
        {
            string strExp = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex r = new Regex(strExp);
            Match m = r.Match(email);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 功能：判断电话号码是否合法
        /// 日期：2013-03-25        
        /// </summary>   
        /// <param name="phone">参数 电话号码</param>
        /// <returns>返回Boolean 合法返回True 不合法返回False</returns>
        public static bool CheckPhoneNum(string phone)
        {
            string strExp = @"(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)";
            Regex r = new Regex(strExp);
            Match m = r.Match(phone);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 功能：邮政编码数据是否合法
        /// 日期：2013-03-25         
        /// </summary>
        /// <param name="postCode">参数 邮政编码</param>
        /// <returns>返回Boolean 合法返回True 不合法返回False</returns>
        public static bool CheckPostCode(string postCode)
        {
            string regExpress = @"^[0-9]{6}$";
            Regex reg = new Regex(regExpress);
            if (reg.IsMatch(postCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 功能：验证手机号码是否合法
        /// 日期：2013-03-25         
        /// </summary>
        /// <param name="mobile">参数 手机号码</param>
        /// <returns>返回Boolean 合法返回True 不合法返回False</returns>
        public static bool CheckMobile(string mobile)
        {
            string regExpress = @"^[0-9]{11}$";
            Regex reg = new Regex(regExpress);
            if (reg.IsMatch(mobile))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 功能：验证身份证号码是否合法
        /// 日期：2013-03-25         
        /// </summary>
        /// <param name="userCardNo">参数 身份证号码</param>
        /// <returns>返回string 合法返回null 不合法返回原因</returns>
        public static string CheckUserCardNo(string userCardNo)
        {
            //如果是15位，则先升级成18位
            if (userCardNo.Length == 15)
            {
                userCardNo = BusinessHelper.UserCarNo15To18(userCardNo);
            }
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            userCardNo = userCardNo.ToLower();
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|x)$");
            System.Text.RegularExpressions.Match mc = rg.Match(userCardNo);
            if (!mc.Success)
            {
                return "身份证号错误：位数必须是18位";
            }
            userCardNo = userCardNo.Replace("x", "a");
            if (aCity[Int32Helper.ToInt32(userCardNo.Substring(0, 2))] == null)
            {
                return "身份证号错误：非法地区";
            }
            try
            {
                DateTime.Parse(userCardNo.Substring(6, 4) + "-" + userCardNo.Substring(10, 2) + "-" + userCardNo.Substring(12, 2));
            }
            catch
            {
                return "身份证号错误：非法生日";
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(userCardNo[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
            }
            if (iSum % 11 != 1)
            {
                return ("身份证号错误：非法证号");
            }
            return null;
        }

        /// <summary>
        /// 功能：检查两个相同类型的对象是否相等
        /// 日期：2013-03-25         
        /// </summary>
        /// <param name="t">参数 类型</param>
        /// <param name="first">第一个参数</param>
        /// <param name="second">第二个参数</param>
        /// <returns>不相等即有变化为false，相等即无变化为True</returns>
        public static bool CheckObjEqual(Type t, object first, object second)
        {
            bool flag = false;
            PropertyInfo[] members = t.GetProperties();
            foreach (PropertyInfo member in members)
            {
                object one = member.GetValue(first, null);
                object two = member.GetValue(second, null);
                flag = EqualObjects(one, two);
                if (!flag)
                {
                    break;
                }
            }
            return flag;
        }
        private static bool EqualObjects(Object one, Object two)
        {
            if (one == null)
            {
                if (two == null || two.ToString() == string.Empty)
                    return true;
                else
                    return false;
            }
            else
            {
                if (two == null)
                {
                    if (string.IsNullOrEmpty(one.ToString().Trim()))
                        return true;
                    else
                        return false;
                }
                else if (one.ToString() != two.ToString())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 功能：判定一个数是否为奇数
        /// 日期：2013-04-01        
        /// </summary>
        /// <param name="num">要验证的数字</param>
        /// <returns>是否是基数</returns>
        public static bool CheckIsEven(int num)
        {
            int s = 1;
            if ((num & s) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
