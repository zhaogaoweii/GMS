using System;
using System.Collections.Generic;
using System.Text;

namespace ZGW.GMS.Core.SMS.Entity
{
    public class MessageDetailE
    {
        private int m_logId;
        private int m_pubId;
        private string m_mobilePhone;
        private string m_sendTime;
        private string m_messageContent;
        private string m_sendStatus;
        private string m_remark;

        public int logId
        {
            get { return m_logId; }
            set { m_logId = value; }
        }
        public int pubId
        {
            get { return m_pubId; }
            set { m_pubId = value; }
        }
        public string mobilePhone
        {
            get { return m_mobilePhone; }
            set { m_mobilePhone = value; }
        }
        public string sendTime
        {
            get { return m_sendTime; }
            set { m_sendTime = value; }
        }
        public string messageContent
        {
            get { return m_messageContent; }
            set { m_messageContent = value; }
        }
        public string sendStatus
        {
            get { return m_sendStatus; }
            set { m_sendStatus = value; }
        }
        public string remark
        {
            get { return m_remark; }
            set { m_remark = value; }
        }
    }
}
