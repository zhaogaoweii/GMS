using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Utility
{
    public static class JsScriptHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipType"></param>
        /// <returns></returns>
        public static string ScritptTip(int tipType, string info)
        {
            StringBuilder sb = new StringBuilder();
            string script = "<script>{0}</script>";
            switch (tipType)
            {
                case 1://提示信息
                    sb.AppendFormat(script, "$(this).showTip(\"" + tipType + "\",\"提示\",\"" + info + "\",function(){})");
                    break;
                case 2://警告
                    sb.AppendFormat(script, "$(this).showTip(\"" + tipType + "\",\"提示\",\"" + info + "\",function(){})");
                    break;
                case 3://错误
                    sb.AppendFormat(script, "$(this).showTip(\"" + tipType + "\",\"提示\",\"" + info + "\",function(){})");
                    break;
                case 4://成功
                    sb.AppendFormat(script, "$(this).showTip(\"" + tipType + "\",\"提示\",\"" + info + "\",function(){})");
                    break;
                case 5: //确认
                    break;
                default:
                    break;
            }
            //sb.Append(@"<script> $(this).showTip(\"5\", \"提示\", \"你确定要删除勾选的记录吗?\", function (){},true);</script>");
            return sb.ToString();
        }
    }
}
