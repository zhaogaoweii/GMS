using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.SMS.Impl
{
    /// <summary>
    /// 短信发送代理类
    /// </summary>
    [ComponentRegistry]
    public class SendAgent : ISendAgent
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobilePhone">短信到达的手机号码</param>
        /// <param name="messageContent">短信内容</param>
        /// <returns>true:成功,false:失败</returns>
        public bool Send(string mobilePhone, string messageContent)
        {
            WSSendMessageSoapClient sendClient = null;

            try
            {

                sendClient = GetPicUploadSoapClient();

                MessageE[] messageEArray = new MessageE[1]
                {
                    new MessageE{ mobilePhone = mobilePhone, messageContent = messageContent}
                };
                return sendClient.sendMessage(messageEArray);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private WSSendMessageSoapClient GetPicUploadSoapClient()
        {
            System.ServiceModel.BasicHttpBinding basicHttpBinding = new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.None);
            basicHttpBinding.CloseTimeout = new TimeSpan(0, 1, 0);
            basicHttpBinding.ReceiveTimeout = new TimeSpan(0, 1, 0);
            basicHttpBinding.SendTimeout = new TimeSpan(0, 1, 0);
            basicHttpBinding.AllowCookies = false;
            basicHttpBinding.BypassProxyOnLocal = false;
            basicHttpBinding.HostNameComparisonMode = System.ServiceModel.HostNameComparisonMode.StrongWildcard;
            basicHttpBinding.MaxBufferSize = 65536;
            basicHttpBinding.MaxBufferPoolSize = 524288;
            basicHttpBinding.MaxReceivedMessageSize = 65536;
            basicHttpBinding.MessageEncoding = System.ServiceModel.WSMessageEncoding.Text;
            basicHttpBinding.TextEncoding = Encoding.UTF8;
            basicHttpBinding.TransferMode = System.ServiceModel.TransferMode.Buffered;
            basicHttpBinding.UseDefaultWebProxy = true;

            basicHttpBinding.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas();
            basicHttpBinding.ReaderQuotas.MaxDepth = 32;
            basicHttpBinding.ReaderQuotas.MaxStringContentLength = 8192;
            basicHttpBinding.ReaderQuotas.MaxArrayLength = 16384;
            basicHttpBinding.ReaderQuotas.MaxBytesPerRead = 4096;
            basicHttpBinding.ReaderQuotas.MaxNameTableCharCount = 16384;

            basicHttpBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.None;
            basicHttpBinding.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;
            basicHttpBinding.Security.Transport.Realm = "";

            basicHttpBinding.Security.Message.ClientCredentialType = System.ServiceModel.BasicHttpMessageCredentialType.UserName;
            basicHttpBinding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

            System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(System.Configuration.ConfigurationManager.AppSettings["SMSSendServiceAddress"]);
            WSSendMessageSoapClient picUploadSoapClient = null;
            try
            {
                picUploadSoapClient = new WSSendMessageSoapClient(basicHttpBinding, endpointAddress);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return picUploadSoapClient;
        }
    }
}
