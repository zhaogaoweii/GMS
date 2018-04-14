using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core.Data;

namespace ZGW.GMS.Core.DBLogging
{
    /// <summary>
    /// 数据日志处理接口
    /// </summary>
    public interface IDBLog
    {
        /// <summary>
        /// 记录数据日志(data限配置了NHibernate映射文件的实体)
        /// </summary>
        /// <param name="userName">操作人人名</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="function">功能名称</param>
        /// <param name="action">操作动作</param>
        /// <param name="data">记录的数据</param>
        void Log(string userName, string moduleName, string function, DBOperation action, DomainEntity data);

        /// <summary>
        /// 记录数据日志(没有配置NHibernate映射文件的实体)
        /// </summary>
        /// <param name="userName">操作人人名</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="function">功能名称</param>
        /// <param name="action">操作动作</param>
        /// <param name="data">记录的数据</param>
        void LogObject(string userName, string moduleName, string function, DBOperation action, object data);

        /// <summary>
        /// 记录数据日志
        /// </summary>
        /// <param name="userName">操作人人名</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="function">功能名称</param>        
        /// <param name="tableName">操作的表名</param>
        /// <param name="recordId">数据记录Id</param>
        /// <param name="action">操作动作</param>
        /// <param name="values">DB操作涉及的数据</param>
        void Log(string userName, string moduleName, string function, string tableName, int recordId, DBOperation action, Dictionary<string, object> values);
    }
}
