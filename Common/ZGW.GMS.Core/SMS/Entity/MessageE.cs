using System;
using System.Collections.Generic;
using System.Text;

namespace ZGW.GMS.Core.SMS.Entity
{
    public class MessageE
    {
        private string m_mobilePhone;
        private string m_messageContent;
        private int m_pubId;
        private int m_sendSys;
        private int m_sendModel;
        private int m_sendMode;
        private string m_sendUser;

        /// <summary>
        /// �ֻ���
        /// </summary>
        public string mobilePhone
        {
            get { return m_mobilePhone; }
            set { m_mobilePhone = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string messageContent
        {
            get { return m_messageContent; }
            set { m_messageContent = value; }
        }

        /// <summary>
        /// ϵͳ����ID
        /// </summary>
        public int pubId
        {
            get { return m_pubId; }
            set { m_pubId = value; }
        }

        /// <summary>
        /// ����ϵͳ
        /// </summary>
        public int sendSys
        {
            get { return m_sendSys; }
            set { m_sendSys = value; }
        }

        /// <summary>
        /// ����ģ��
        /// </summary>
        public int sendModel
        {
            get { return m_sendModel; }
            set { m_sendModel = value; }
        }

        /// <summary>
        /// ����ģʽ
        /// </summary>
        public int sendMode
        {
            get { return m_sendMode; }
            set { m_sendMode = value; }
        }

        /// <summary>
        /// ϵͳ�û�
        /// </summary>
        public string sendUser
        {
            get { return m_sendUser; }
            set { m_sendUser = value; }
        }
    }
}
