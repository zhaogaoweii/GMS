using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace CIIC.OMS.Core.Messaging
{
    internal class DefaultMQ:MessageBase,IMessage
    {
        private readonly Message message;
        private object data;

        public DefaultMQ(Message _message)
        {
            this.message = _message;
        }

        public override void Acknowledge()
        {
            throw new Exception("未实现本逻辑");
        }

        public override object Data
        {
            get
            {
                if (null==data)
                {
                    data = message.Body;
                }
                return data;
            }
        }
    }
}
