using System;
namespace ZGW.GMS.Core.SMS
{
    /// <summary>
    /// 发送短信代理类
    /// </summary>
    public interface ISendAgent
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobilePhone">短信到达的手机号码</param>
        /// <param name="messageContent">短信内容</param>
        /// <returns>true:成功,false:失败</returns>
        bool Send(string mobilePhone, string messageContent);
    }
}
