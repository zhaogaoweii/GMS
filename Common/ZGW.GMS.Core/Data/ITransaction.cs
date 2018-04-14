using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 事务访问接口
    /// </summary>
    public interface ITransaction:IDisposable
    {
        /// <summary>
        /// 当前事务是否激活
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// 事务是否已经提交
        /// </summary>
        bool WasCommitted { get; }

        /// <summary>
        /// 事务是否已经回滚
        /// </summary>
        bool WasRolledBack { get; }

        /// <summary>
        /// 开始事务
        /// </summary>
        void Begin();

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="isolationLevel">事务等级</param>
        void Begin(IsolationLevel isolationLevel);

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
    }
}
