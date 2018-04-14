using System;
using System.Threading;
using MDS;
using MDS.Client;

namespace ZGW.GMS.Core.Messaging
{
    /// <summary>
    /// 基于DotNetMQ的消息队列处理器
    /// </summary>
    internal class DotNetMQHandler : IMessageQueueHandler, IDisposable
    {
        private MDSClient client;
        private MessageReceivedHandler messageReceived;

        public DotNetMQHandler(string applicationName, string ipAddress,int port)
        {
            client = new MDSClient(applicationName, ipAddress, port);
            client.MessageReceived += client_MessageReceived;
            client.Connect();
        }

        public void Send(object message)
        {
            var outgoingMessage = client.CreateMessage();
            outgoingMessage.MessageData = GeneralHelper.SerializeObject(message);
            try
            {
                outgoingMessage.Send();
            }
            finally
            {
                client.Connect();
                outgoingMessage.Send();
            }
        }

        public event MessageReceivedHandler MessageReceived
        {
            add { messageReceived += value; }
            remove { messageReceived -= value; }
        }

        protected virtual void OnReceived(object sender, MessageReceivedEventArgs args)
        {
            if (messageReceived != null)
            {
                messageReceived(sender, args);
            }
        }

        void client_MessageReceived(object sender, MDS.Client.MessageReceivedEventArgs e)
        {
            var instance=GeneralHelper.DeserializeObject(e.Message.MessageData);
            IMessage message=new DotNetMQ(e.Message);
            var eventArgs=new MessageReceivedEventArgs(this,message);
            OnReceived(this, eventArgs);
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Disconnect();
                client.Dispose();
                client= null;
            }
        }
    }
}
