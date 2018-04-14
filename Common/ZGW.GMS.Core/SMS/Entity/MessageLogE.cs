using System;
using System.Collections.Generic;
using System.Text;

namespace ZGW.GMS.Core.SMS.Entity
{
    public class MessageLogE
    {
        private string m_sendTime;
        private string m_sendUser;
        private int m_sendSys;
        private int m_sendModel;
        private int m_sumNum;
        private int m_sucNum;
        private int m_failNum;
        private string m_remark;
        private int m_sendMode;

        public string sendTime
        {
            get { return m_sendTime; }
            set { m_sendTime = value; }
        }
        public string sendUser
        {
            get { return m_sendUser; }
            set { m_sendUser = value; }
        }
        public int sendSys
        {
            get { return m_sendSys; }
            set { m_sendSys = value; }
        }
        public int sendModel
        {
            get { return m_sendModel; }
            set { m_sendModel = value; }
        }
        public int sumNum
        {
            get { return m_sumNum; }
            set { m_sumNum = value; }
        }
        public int sucNum
        {
            get { return m_sucNum; }
            set { m_sucNum = value; }
        }
        public int failNum
        {
            get { return m_failNum; }
            set { m_failNum = value; }
        }
        public string remark
        {
            get { return m_remark; }
            set { m_remark = value; }
        }
        public int sendMode
        {
            get { return m_sendMode; }
            set { m_sendMode = value; }
        }
    }
}
