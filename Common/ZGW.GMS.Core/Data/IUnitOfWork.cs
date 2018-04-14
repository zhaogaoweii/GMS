using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 注册一个新实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void RegisterNew<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 恢复一个实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void RegisterUnchanged<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void RegisterChanged<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>查询对象</returns>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
        
        /// <summary>
        /// 获取与id对应的实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="id">查询的ID</param>
        /// <returns>与ID对应的实体</returns>
        TEntity Get<TEntity>(object id) where TEntity : class;

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns>事务对象</returns>
        ITransaction BeginTransaction();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">事务等级</param>
        /// <returns>事务对象</returns>
        ITransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// 恢复当前工作单元
        /// </summary>
        void Rollback();

        /// <summary>
        /// 提交工作单元
        /// </summary>
        void Commit();

        /// <summary>
        /// 数据上下文
        /// </summary>
        dynamic Session { get; }
    }
}
