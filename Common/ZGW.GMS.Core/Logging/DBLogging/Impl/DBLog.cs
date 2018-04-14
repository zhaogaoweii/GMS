using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core.Data;
using ZGW.GMS.Core;

using System.Runtime.Serialization;

namespace ZGW.GMS.Core.DBLogging
{
    ///// <summary>
    ///// 数据日志实现类
    ///// </summary>
    //[ComponentRegistry(Lifetime.Singleton)]
    //public class DBLog : IDBLog
    //{
    //    //        private ISessionFactory sessionFactory = NHSessionFactoryProvider.Instance.GetSessionFactory();
    //    //        private IDBLogHandler handler;

    //    //        /// <summary>
    //    //        /// 构造函数
    //    //        /// </summary>
    //    //        /// <param name="handler">日志的处理者</param>
    //    //        public DBLog(IDBLogHandler handler)
    //    //        {
    //    //            this.handler = handler;
    //    //        }

    //    //        /// <summary>
    //    //        /// 记录数据日志
    //    //        /// </summary>
    //    //        /// <param name="userName">操作人</param>
    //    //        /// <param name="moduleName">模块名称</param>
    //    //        /// <param name="function">功能名称</param>
    //    //        /// <param name="action">操作动作</param>
    //    //        /// <param name="data">记录的数据</param>
    //    //        public void Log(string userName, string moduleName, string function, DBOperation action, DomainEntity data)
    //    //        {
    //    //            if (data == null)
    //    //            {
    //    //                throw new ArgumentNullException("参数data不能为空");
    //    //            }

    //    //            if (!data.GetType().IsDefined(typeof(SerializableAttribute), false) 
    //    //                && !typeof(ISerializable).IsAssignableFrom(data.GetType()))
    //    //            {
    //    //                throw new ArgumentException("参数data为不可序列化的数据");
    //    //            }

    //    //            data = Clone(data);

    //    //            HistoryRecord record = new HistoryRecord()
    //    //            {
    //    //                UserName = userName,
    //    //                Module = moduleName,
    //    //                Func = function,
    //    //                Action = (int)action,
    //    //                TableName = GetTableName(data),
    //    //                RecordID = data.Id,
    //    //                Content = XmlHelper.SerializeToXml(data),
    //    //                UpdateTime = DateTime.Now
    //    //            };

    //    //            handler.Handle(record);

    //    //            //DBHistory his = new DBHistory();
    //    //            //his.UserName = userName;
    //    //            //his.Module = moduleName;
    //    //            //his.Func = func;
    //    //            //his.Action = (int)action;
    //    //            //his.RecordID = data.Id;

    //    //            //his.TableName = GetTableName(data);
    //    //            //_repository.UnitOfWork.BeginTransaction();
    //    //            //_repository.Create(his);
    //    //            ////添加数据详细内容
    //    //            //AddDetails(_repository.UnitOfWork, his, data);
    //    //            //_repository.Flush();
    //    //        }

    //    //        /// <summary>
    //    //        /// 记录数据日志(data限配置了NHibernate映射文件的实体)
    //    //        /// </summary>
    //    //        /// <param name="userName">操作人人名</param>
    //    //        /// <param name="moduleName">模块名称</param>
    //    //        /// <param name="function">功能名称</param>
    //    //        /// <param name="action">操作动作</param>
    //    //        /// <param name="data">记录的数据</param>
    //    //        public void LogObject(string userName, string moduleName, string function, DBOperation action, object data)
    //    //        {
    //    //            if (data == null)
    //    //            {
    //    //                throw new ArgumentNullException("参数data不能为空");
    //    //            }

    //    //            if (!data.GetType().IsDefined(typeof(SerializableAttribute),false) 
    //    //                && !typeof(ISerializable).IsAssignableFrom(data.GetType()))
    //    //            {
    //    //                throw new ArgumentException("参数data为不可序列化的数据");
    //    //            }

    //    //            HistoryRecord record = new HistoryRecord()
    //    //            {
    //    //                UserName = userName,
    //    //                Module = moduleName,
    //    //                Func = function,
    //    //                Action = (int)action,
    //    //                Content = XmlHelper.SerializeToXml(data),
    //    //                UpdateTime = DateTime.Now
    //    //            };

    //    //            handler.Handle(record);
    //    //        }

    //    //        /// <summary>
    //    //        /// 记录数据日志
    //    //        /// </summary>
    //    //        /// <param name="userName">操作人人名</param>
    //    //        /// <param name="moduleName">模块名称</param>
    //    //        /// <param name="function">功能名称</param>        
    //    //        /// <param name="tableName">操作的表名</param>
    //    //        /// <param name="recordId">数据记录Id</param>
    //    //        /// <param name="action">操作动作</param>
    //    //        /// <param name="values">DB操作涉及的数据</param>
    //    //        public void Log(string userName, string moduleName, string function, string tableName, int recordId, DBOperation action, Dictionary<string, object> values)
    //    //        {
    //    //            HistoryRecord record = new HistoryRecord()
    //    //            {
    //    //                UserName = userName,
    //    //                Module = moduleName,
    //    //                Func = function,
    //    //                Action = (int)action,
    //    //                TableName = tableName,
    //    //                RecordID = recordId,
    //    //                Content = XmlHelper.ConverDictionaryToXml(values),
    //    //                UpdateTime = DateTime.Now
    //    //            };

    //    //            handler.Handle(record);
    //    //        }

    //    //        /// <summary>
    //    //        /// 克隆数据
    //    //        /// </summary>
    //    //        /// <param name="source">源数据</param>
    //    //        private DomainEntity Clone(object source)
    //    //        {
    //    //            var sourceType = source.GetType();
    //    //            var metadata = sessionFactory.GetClassMetadata(source.GetType());
    //    //            var values = metadata.GetPropertyValues(source, EntityMode.Poco);
    //    //            var target = Activator.CreateInstance(sourceType);
    //    //            metadata.SetPropertyValues(target, values, EntityMode.Poco);
    //    //            return target as DomainEntity;
    //    //        }

    //    //        /// <summary>
    //    //        /// 取得当前数据的表名
    //    //        /// </summary>
    //    //        /// <param name="source">源数据</param>
    //    //        /// <returns>表名</returns>
    //    //        private string GetTableName(object source)
    //    //        {
    //    //            var classMeta = sessionFactory.GetClassMetadata(source.GetType());
    //    //            return ((SingleTableEntityPersister)classMeta).TableName;
    //    //        }

    //    //        ///// <summary>
    //    //        ///// 添加详细内容
    //    //        ///// </summary>
    //    //        ///// <param name="unitOfWork"></param>
    //    //        ///// <param name="history"></param>
    //    //        ///// <param name="data"></param>
    //    //        //private void AddDetails(IUnitOfWork unitOfWork, DBHistory history, object data)
    //    //        //{
    //    //        //    string xml = XmlHelper.SerializeToXml(data);

    //    //        //    int startIndex = 0;
    //    //        //    int contentLength = 2000;
    //    //        //    while (startIndex < xml.Length)
    //    //        //    {
    //    //        //        DBHistoryDetail detail = new DBHistoryDetail();
    //    //        //        if (xml.Length - startIndex > contentLength)
    //    //        //        {
    //    //        //            detail.Content = xml.Substring(startIndex, contentLength);
    //    //        //        }
    //    //        //        else
    //    //        //        {
    //    //        //            detail.Content = xml.Substring(startIndex);
    //    //        //        }
    //    //        //        history.Details.Add(detail);
    //    //        //        _repository.UnitOfWork.RegisterNew(detail);
    //    //        //        startIndex += contentLength;
    //    //        //    }
    //    //        //}
    //    //    }
    //    //}
    //}
}