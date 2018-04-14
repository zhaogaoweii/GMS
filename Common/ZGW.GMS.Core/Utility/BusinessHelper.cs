using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Data;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 与业务有关的公共方法
    /// </summary>
    public class BusinessHelper
    {
        /// <summary>
        /// 加载应用程序的程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] LoadAppAssemblies()
        {
            string binFolder = AppDomain.CurrentDomain.RelativeSearchPath;
            if (String.IsNullOrEmpty(binFolder))
                binFolder = Environment.CurrentDirectory;
            DirectoryInfo binInfo = new DirectoryInfo(binFolder);
            string[] files = binInfo.GetFiles("ZGW.GMS.Core.*.dll").Select(m => m.Name).ToArray();

            return files.Select(m => Assembly.Load(Path.GetFileNameWithoutExtension(m)))
                .ToArray();
        }

        /// <summary>
        /// 功能：根据身份证号码取得性别
        /// 日期：2013-04-01        
        /// </summary>
        /// <param name="card_code">身份证号码</param>
        /// <returns>性别 true为男false为女</returns>
        public static bool GetGenderByCardCode(string card_code)
        {            
            int idLength = card_code.Length;
            return idLength == 15 ? ValidationHelper.CheckIsEven(Convert.ToInt32(card_code.Substring(idLength - 1, 1))) : ValidationHelper.CheckIsEven(Convert.ToInt32(card_code.Substring(idLength - 2, 1)));
        }
       
        /// <summary>
        /// 功能：四舍五入方法
        /// 日期：2013-03-25
        /// </summary>
        /// <param name="value">需要处理的对象</param>
        /// <param name="digit">保留位数</param>
        /// <returns>四舍五入之后的结果</returns>
        public static decimal MathRound(decimal value, int digit)
        {
            decimal vt = Convert.ToDecimal(Math.Pow(10, digit));
            decimal vx = value * vt;
            vx = Math.Round(vx, 2);
            vx += 0.5M;
            return Convert.ToDecimal(Math.Floor((double)vx)) / vt;
        }

        /// <summary>
        /// 功能：四舍五入方法(不保留小数部分)
        /// 日期：2013-03-25
        /// </summary>     
        /// <param name="decData">需要处理的对象</param>
        /// <returns>四舍五入之后的结果</returns>
        public static int MathRound(decimal decData)
        {
            //四舍五入后的返回值
            int iResult = 0;
            string strFee = decData.ToString();
            //小数点前的整数部分
            string strInt = "";
            //小数点后的第一位小数
            string strTail = "";
            //小数点位置
            int iPoint = -1;
            for (int i = 0; i < strFee.Length; i++)
            {
                if (strFee[i] == '.')
                {
                    iPoint = i;
                    break;
                }
            }
            //判断小数点位置
            if (iPoint == -1)
            {
                //没有小数点，为整数
                iResult = int.Parse(decData.ToString());
            }
            else
            {

                //整数部分
                strInt = strFee.Substring(0, iPoint);
                //小数点后第一位数
                strTail = strFee.Substring(iPoint + 1, 1);

                if (strTail.Equals(""))
                {
                    iResult = int.Parse(strInt);
                }
                else
                {
                    if (int.Parse(strTail) > 4)
                    {
                        //返回5入后的值
                        iResult = int.Parse(strInt) + 1;
                    }
                    else
                    {
                        iResult = int.Parse(strInt);
                    }
                }
            }

            return iResult;
        }

        /// <summary>
        /// 功能：将金额转成大写   
        /// 日期：2013-04-01        
        /// </summary>   
        /// <param name="money">要转换的金额</param>   
        /// <returns>大写金额</returns>   
        public static string GetMoneyToChinese(string money)
        {
            string 功能ReturnValue = null;
            bool IsNegative = false; // 是否是负数   
            if (money.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数   
                money = money.Trim().Remove(0, 1);
                IsNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数 123.489→123.49　　123.4→123.4   
            money = Math.Round(double.Parse(money), 2).ToString();
            if (money.IndexOf(".") > 0)
            {
                if (money.IndexOf(".") == money.Length - 2)
                {
                    money = money + "0";
                }
            }
            else
            {
                money = money + ".00";
            }
            strLower = money;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "圆";
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "壹";
                        break;
                    case "2":
                        strUpart = "贰";
                        break;
                    case "3":
                        strUpart = "叁";
                        break;
                    case "4":
                        strUpart = "肆";
                        break;
                    case "5":
                        strUpart = "伍";
                        break;
                    case "6":
                        strUpart = "陆";
                        break;
                    case "7":
                        strUpart = "柒";
                        break;
                    case "8":
                        strUpart = "捌";
                        break;
                    case "9":
                        strUpart = "玖";
                        break;
                }

                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "分";
                        break;
                    case 2:
                        strUpart = strUpart + "角";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "拾";
                        break;
                    case 6:
                        strUpart = strUpart + "佰";
                        break;
                    case 7:
                        strUpart = strUpart + "仟";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "拾";
                        break;
                    case 10:
                        strUpart = strUpart + "佰";
                        break;
                    case 11:
                        strUpart = strUpart + "仟";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "拾";
                        break;
                    case 14:
                        strUpart = strUpart + "佰";
                        break;
                    case 15:
                        strUpart = strUpart + "仟";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }

                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }
            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零圆", "万圆");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零圆", "圆");
            strUpper = strUpper.Replace("零零", "零");
            // 对壹圆以下的金额的处理   
            if (strUpper.Substring(0, 1) == "圆")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零圆整";
            }
            功能ReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "负" + 功能ReturnValue;
            }
            else
            {
                return 功能ReturnValue;
            }
        }

          /// <summary>
        /// 功能：根据公积金申报基数由规则得到公积金申报缴存额
        /// 日期：2013-03-25        
        /// </summary>
        /// <param name="gjjBase">公积申报基数</param>
        /// <returns>公积金申报缴存额</returns>
        /// 公积金缴存额计算规则：公积金基数乘以12％后四舍五入到整数，再乘以2，生成缴存额。计算的缴存额不允许超过上限，不能小于0
        public static int GetGjjFeeByBase(decimal gjjBase)
        {
            decimal dRate = DecimalHelper.ToDecimal(WebConfigHelper.GetConfigurationApp("GjjFeeCalculator"));//0.12
            decimal decRate = decimal.Parse(dRate.ToString());
            decimal decData = gjjBase * decRate;
            int iR = Convert.ToInt32(MathRound(decData, 2));
            return iR * 2;
        }

        /// <summary>
        /// 功能：将15位的身份证号码转成18位的身份证号码
        /// 日期：2013-03-25         
        /// </summary>
        /// <param name="cardNo">15位的身份证号码</param>
        /// <returns>18位的身份证号码</returns>
        public static string UserCarNo15To18(string cardNo)
        {
            int iS = 0;
            //加权因子常数 
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //校验码常数 
            string LastCode = "10X98765432";
            //新身份证号 
            string perIDNew;
            perIDNew = cardNo.Substring(0, 6);
            //填在第6位及第7位上填上‘1’，‘9’两个数字 
            perIDNew += "19";
            perIDNew += cardNo.Substring(6, 9);
            //进行加权求和 
            for (int i = 0; i < 17; i++)
            {
                iS += Int32Helper.ToInt32(perIDNew.Substring(i, 1)) * iW[i];
            }
            //取模运算，得到模值 
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。 
            perIDNew += LastCode.Substring(iY, 1);
            return perIDNew;
        }

        /// <summary>
        /// 功能:将值转化为bool类型(假定该值是可以转化为bool类型的bit类型，即0和1)
        /// 日期：2013-03-25        
        /// </summary>
        /// <param name="objBool">要转的对象</param>
        /// <returns>返回Boolean 0、DBNull、NULL 返回False</returns>
        public static bool ToBool(object objBool)
        {
            if (objBool == null) return false;
            if (objBool == DBNull.Value) return false;
            return objBool.ToString().Trim() != "0";
        }

        /// <summary>
        /// 功能:金额格式化
        /// 日期：2013-03-25        
        /// </summary>   
        /// <param name="obj">要转的对象</param>
        /// <returns>金额（88.00）</returns>
        public static string MoneyFormat(object obj)
        {
            return string.Format("{0:N}", obj == null || string.IsNullOrEmpty(obj.ToString()) ? 0 : obj).Replace(",", "");
        }

        /// <summary>
        /// 功能:转成百分比
        /// 日期：2013-03-25        
        /// </summary>
        /// <param name="obj">要转成百分比的对象</param>
        /// <returns>转成百分比之后的对象</returns>
        public static string ToPercent(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null || string.IsNullOrEmpty(obj.ToString()) || obj.ToString() == "0")
            {
                return "0";
            }
            return (DecimalHelper.ToDecimal(obj) * 100) + "%";
        }

        /// <summary>
        /// 功能：将参数按照【见分进角】的规则计算并返回
        /// 日期：2013-04-02
        /// </summary>
        /// <param name="obj">要转成见分进角的对象</param>
        /// <returns>见分进角后的结果</returns>
        public static decimal GetJianFenJinJiao(object obj)
        {
            decimal value = DecimalHelper.ToDecimal(obj);
            value = decimal.Floor(value * 10 + 1m);
            value = value / 10;
            return value;
        }

        /// <summary>
        /// 功能：将参数按照【见角进元】的规则计算并返回
        /// 日期：2013-04-02
        /// </summary>        
        /// <param name="obj">要计算的对象</param>
        /// <returns>计算后的结果</returns>
        public static decimal GetJianJiaoJinYuan(object obj)
        {
            decimal value = DecimalHelper.ToDecimal(obj);
            value = decimal.Floor(value + 1m);
            return value;
        }

        /// <summary>
        /// 功能：将参数按照【四舍五入】的规则计算并返回
        /// 日期：2013-04-02
        /// </summary>        
        /// <param name="obj">要计算的对象</param>
        /// <returns>计算后的结果</returns>
        public static decimal GetSiSheWuRu(object obj)
        {
            decimal value = DecimalHelper.ToDecimal(obj);
            decimal vt = Convert.ToDecimal(Math.Pow(10, 2));
            decimal vx = value * vt;
            vx = Math.Round(vx, 2);
            vx += 0.5M;
            value = Convert.ToDecimal(Math.Floor((double)vx)) / vt;
            return value;
        }

        /// <summary>
        /// 功能：将参数按照【四舍五入到分】的规则计算并返回
        /// 日期：2013-04-02
        /// </summary>        
        /// <param name="obj">要计算的对象</param>
        /// <returns>计算后的结果</returns>
        public static decimal GetSiSheWuRuToFen(object obj)
        {
            decimal value = DecimalHelper.ToDecimal(obj);
            return BusinessHelper.MathRound(value, 2);
        }

        /// <summary>
        /// 功能：将参数按照【四舍五入到角】的规则计算并返回
        /// 日期：2013-04-02
        /// </summary>        
        /// <param name="obj">要计算的对象</param>
        /// <returns>计算后的结果</returns>
        public static decimal GetSiSheWuRuToJiao(object obj)
        {
            decimal value = DecimalHelper.ToDecimal(obj);
            return BusinessHelper.MathRound(value, 1);
        }

        /// <summary>
        /// 功能：将参数按照【四舍五入到元】的规则计算并返回
        /// 日期：2013-04-02
        /// </summary>        
        /// <param name="obj">要计算的对象</param>
        /// <returns>计算后的结果</returns>
        public static decimal GetSiSheWuRuToYuan(object obj)
        {
            decimal value = DecimalHelper.ToDecimal(obj);
            return BusinessHelper.MathRound(value, 0); 
        }

        /// <summary>
        /// 功能：将参数按照【舍去分】的规则计算并返回
        /// 日期：2013-04-02
        /// </summary>        
        /// <param name="obj">要计算的对象</param>
        /// <returns>计算后的结果</returns>
        public static decimal GetSheQuFen(object obj)
        {
            decimal value = DecimalHelper.ToDecimal(obj);
            value = value * 10;
            value = Math.Floor(value);
            value /= 10;
            return value;
        }

        /// <summary>
        /// 功能：将参数按照【舍去角】的规则计算并返回
        /// 日期：2013-04-02
        /// </summary>        
        /// <param name="obj">要计算的对象</param>
        /// <returns>计算后的结果</returns>
        public static decimal GetSheQuJiao(object obj)
        {
            decimal value = DecimalHelper.ToDecimal(obj);
            return Math.Floor(value);
        }

        /// <summary>
        /// 功能：取得数据源中数据的总行数
        /// 日期：2013-04-02
        /// </summary>        
        /// <param name="obj">数据源</param>
        /// <returns>数据源行数</returns>
        public static int GetRowTotal(DataTable dt)
        {
            if (ValidationHelper.CheckDt(dt))
            {
                return Int32Helper.ToInt32(dt.Rows[0]["ROW_TOTAL"]);
            }
            return 0;
        }


        /// <summary>
        /// 是否运行oms二期改造代码标识
        /// </summary>
        public static bool IsExeOms2
        {
            get
            {
                try
                {
                    string isExeOms = ConfigureHelper.GetAppSetting("IsExeOms2");
                    return isExeOms.ToInt(0) == 1 ? true : false;

                }
                catch
                {
                    return false;
                }

            }
        }

    } 
}
