using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.SMS
{
    /// <summary>
    /// 短信发送接口
    /// </summary>
    public interface ISMS
    {
        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="messageContent">短信内容</param>
        /// <returns>true:成功，false:失败</returns>
        bool Send(string mobilePhone, string messageContent);
    }
}
