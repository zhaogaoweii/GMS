using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core.Data;
using ZGW.GMS.Core.SMS.Entity;

namespace ZGW.GMS.Core.SMS.Impl
{
//    /// <summary>
//    /// 发送发送服务
//    /// </summary>
//    [ComponentRegistry]
//    public class SMSImpl : ISMS
//    {
//        /// <summary>
//        /// 短信发送
//        /// </summary>
//        /// <param name="mobilePhone">手机号码</param>
//        /// <param name="messageContent">短信内容</param>
//        /// <returns>true:成功，false:失败</returns>
//        public bool Send(string mobilePhone, string messageContent)
//        {
//            return true;//无短息服务暂时先将功能去掉2014-12-10
//            //MessageE[] msgArray = new MessageE[1]
//            //{
//            //    new MessageE{mobilePhone = mobilePhone, messageContent = messageContent , sendUser = "SYS"}
//            //};

//            //return SendMessageAndLog(msgArray);
//        }

//        private bool SendMessageAndLog(MessageE[] MsgArrE)
//        {
//            MessageLogE MsgLogE = new MessageLogE();
//            MessageDetailE[] MsgDArrE = new MessageDetailE[MsgArrE.Length];
//            int recordCount = MsgArrE.Length;
//            int recordSuc = 0;
//            int recordFail = 0;
//            for (int i = 0; i < MsgArrE.Length; i++)
//            {
//                MessageE MsgE = MsgArrE[i];
//                //添加详细信息实体
//                MessageDetailE MsgDetailE = new MessageDetailE();
//                MsgDetailE.messageContent = MsgE.messageContent;
//                MsgDetailE.mobilePhone = MsgE.mobilePhone;
//                MsgDetailE.pubId = MsgE.pubId;
//                MsgDetailE.sendTime = DateTime.Now.ToString();
//                MsgDetailE.sendStatus = "999";
//                MsgDArrE[i] = MsgDetailE;
//            }
//            //添加日志信息实体
//            MessageE MLogE = (MessageE)MsgArrE[0];
//            MsgLogE.sendTime = DateTime.Now.ToString();
//            MsgLogE.sendUser = MLogE.sendUser;
//            MsgLogE.sendSys = MLogE.sendSys;
//            MsgLogE.sendModel = MLogE.sendModel;
//            MsgLogE.sumNum = recordCount;
//            MsgLogE.sucNum = recordSuc;
//            MsgLogE.failNum = recordFail;
//            MsgLogE.sendMode = MLogE.sendMode;
//            MsgLogE.remark = string.Empty;

//            return Add(MsgDArrE, MsgLogE);
//        }

//        private bool Add(MessageDetailE[] msgDetailE, MessageLogE msgLogE)
//        {
//            Dictionary<string, List<ParameterEntity>> dicKvPair = new Dictionary<string, List<ParameterEntity>>();
//            int logId = 0;
//            var logAdd = LogAdd(msgLogE, out logId);
//            dicKvPair.Add(logAdd.Key + "/*" + logId + "*/", logAdd.Value);

//            foreach (var detail in msgDetailE)
//            {
//                int detailId = 0;
//                detail.logId = logId;
//                var detailAdd = DetailAdd(detail, out detailId);
//                dicKvPair.Add(detailAdd.Key + "/*" + detailId + "*/", detailAdd.Value);
//            }
//            List<object> results = null;
//            try
//            {
//                results = OracleDBHelper.ExecuteDBTransaction(dicKvPair);

//                return (results != null && results[0] != null && Convert.ToInt32(results[0]) > 0) ? true : false;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        private KeyValuePair<string, List<ParameterEntity>> LogAdd(MessageLogE msgLogE, out int id)
//        {
//            int logId = OracleDBHelper.SelectTBID("SEQ_MESSAGE_LOG", DataBaseType.DefaultDB);
//            id = logId;

//            string sql =
//                @"insert into message_log
//                  (log_id, send_time, send_user, send_sys, send_model, sum_num, suc_num, fail_num)
//                values
//                  (:v_log_id, :v_send_time, :v_send_user, :v_send_sys, :v_send_model, :v_sum_num, :v_suc_num, :v_fail_num)";

//            List<ParameterEntity> parList = new List<ParameterEntity>
//            {
//               new ParameterEntity(){ Name =":v_log_id",Value = logId,DbType =DbType.Int32},
//               new ParameterEntity(){ Name =":v_send_time",Value = msgLogE.sendTime,DbType =DbType.Date},
//               new ParameterEntity(){ Name =":v_send_user",Value = msgLogE.sendUser,DbType =DbType.String},
//               new ParameterEntity(){ Name =":v_send_sys",Value = msgLogE.sendSys,DbType =DbType.Int32},
//               new ParameterEntity(){ Name =":v_send_model",Value = msgLogE.sendModel,DbType =DbType.Int32},
//               new ParameterEntity(){ Name =":v_sum_num",Value = msgLogE.sumNum,DbType =DbType.Int32},
//               new ParameterEntity(){ Name =":v_suc_num",Value = msgLogE.sucNum,DbType =DbType.Int32},
//               new ParameterEntity(){ Name =":v_fail_num",Value = msgLogE.failNum,DbType =DbType.Int32}
//            };

//            return new KeyValuePair<string, List<ParameterEntity>>(sql, parList);
//        }

//        private KeyValuePair<string, List<ParameterEntity>> DetailAdd(MessageDetailE msgDetailE, out int id)
//        {
//            int detailId = OracleDBHelper.SelectTBID("SEQ_MESSAGE_DETAIL", DataBaseType.DefaultDB);
//            id = detailId;
//            string sql =
//                @"insert into message_detail
//                  (detail_id, log_id, pub_id, mobile_phone, send_time, message_content, send_status)
//                values
//                  (:v_detail_id, :v_log_id, :v_pub_id, :v_mobile_phone, :v_send_time, :v_message_content, :v_send_status)
//                ";

//            List<ParameterEntity> parList = new List<ParameterEntity>
//            {
//               new ParameterEntity(){ Name =":v_detail_id",Value = detailId,DbType =DbType.Int32},
//               new ParameterEntity(){ Name =":v_log_id",Value = msgDetailE.logId,DbType =DbType.Int32},
//               new ParameterEntity(){ Name =":v_pub_id",Value = msgDetailE.pubId,DbType =DbType.Int32},
//               new ParameterEntity(){ Name =":v_mobile_phone",Value = msgDetailE.mobilePhone,DbType =DbType.String},
//               new ParameterEntity(){ Name =":v_send_time",Value = msgDetailE.sendTime,DbType =DbType.Date},
//               new ParameterEntity(){ Name =":v_message_content",Value = msgDetailE.messageContent,DbType =DbType.String},
//               new ParameterEntity(){ Name =":v_send_status",Value = msgDetailE.sendStatus,DbType =DbType.String},
//            };

//            return new KeyValuePair<string, List<ParameterEntity>>(sql, parList);
//        }
//    }
}
