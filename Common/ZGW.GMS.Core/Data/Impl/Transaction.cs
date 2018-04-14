using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Data
{
    ///// <summary>
    ///// 基于NHibernate的事务接口实现
    ///// </summary>
    //internal class NHTransaction:ITransaction
    //{
    //    private NHibernate.ITransaction transaction;
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="transaction">NHibernate的事务接口</param>
    //    public NHTransaction(NHibernate.ITransaction transaction)
    //    {
    //        this.transaction=transaction;
    //    }

    //    /// <summary>
    //    /// 当前事务是否激活
    //    /// </summary>
    //    public bool IsActive
    //    {
    //        get { return transaction.IsActive; }
    //    }

    //    /// <summary>
    //    /// 事务是否已经提交
    //    /// </summary>
    //    public bool WasCommitted
    //    {
    //        get { return transaction.WasCommitted; }
    //    }

    //    /// <summary>
    //    /// 事务是否已经回滚
    //    /// </summary>
    //    public bool WasRolledBack
    //    {
    //        get { return transaction.WasRolledBack; }
    //    }

    //    /// <summary>
    //    /// 开始事务
    //    /// </summary>
    //    public void Begin()
    //    {
    //        transaction.Begin();
    //    }

    //    /// <summary>
    //    /// 开始事务
    //    /// </summary>
    //    /// <param name="isolationLevel">事务等级</param>
    //    public void Begin(System.Data.IsolationLevel isolationLevel)
    //    {
    //        transaction.Begin(isolationLevel);
    //    }

    //    /// <summary>
    //    /// 提交事务
    //    /// </summary>
    //    public void Commit()
    //    {
    //        transaction.Commit();
    //    }

    //    /// <summary>
    //    /// 回滚事务
    //    /// </summary>
    //    public void Rollback()
    //    {
    //        transaction.Rollback();
    //    }

    //    public void Dispose()
    //    {
    //        if (transaction.IsActive)
    //        {
    //            transaction.Rollback();
    //        }
    //        transaction.Dispose();
    //    }
    //}
}
