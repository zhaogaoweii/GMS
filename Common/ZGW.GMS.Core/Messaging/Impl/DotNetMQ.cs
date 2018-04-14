using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDS;
using MDS.Client;

namespace ZGW.GMS.Core.Messaging
{
    internal class DotNetMQ:MessageBase,IMessage
    {
        private readonly IIncomingMessage message;

        private object data;

        public DotNetMQ(IIncomingMessage message)
        {
            this.message = message;
        }

        public override void Acknowledge()
        {
            message.Acknowledge();
        }

        public override object Data
        {
            get
            {
                if (data == null)
                {
                    data = GeneralHelper.DeserializeObject(message.MessageData);
                }
                return data;
            }
        }
    }
}
