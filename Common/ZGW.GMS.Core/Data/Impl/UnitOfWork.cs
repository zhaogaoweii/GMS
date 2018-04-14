using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq.Expressions;
using NHibernate.Linq;
using System.Data;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 注册工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ISessionFactory sessionFactory;
        private ISession currentSession;
        private bool disposed;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sessionFactory">ISessionFactory对象</param>
        public UnitOfWork(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
            this.currentSession = sessionFactory.OpenSession();
        }

        /// <summary>
        /// 开启一个新的工作单元
        /// </summary>
        /// <param name="dbCategory">数据分类</param>
        /// <returns>IUnitOfWork</returns>
        public static IUnitOfWork Start(DBCatetory dbCategory = DBCatetory.Production)
        {
            return ObjectContainer.ResolveService<IUnitOfWork>(new Dictionary<string, object> { { "DBCategory", dbCategory } });
        }

        /// <summary>
        /// 注册一个新实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class
        {
            currentSession.Save(entity);
        }

        /// <summary>
        /// 恢复一个实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void RegisterUnchanged<TEntity>(TEntity entity) where TEntity : class
        {
            currentSession.Refresh(entity);
        }

        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void RegisterChanged<TEntity>(TEntity entity) where TEntity : class
        {
            //currentSession.Evict(entity);
            currentSession.SaveOrUpdate(entity);
        }

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            currentSession.Delete(entity);
        }

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>IQueryable对象</returns>
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return currentSession.Query<TEntity>();
        }

        /// <summary>
        /// 获取与id对应的实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="id">查询的ID</param>
        /// <returns>与ID对应的实体</returns>
        public TEntity Get<TEntity>(object id) where TEntity : class
        {
            return currentSession.Get<TEntity>(id);
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns>事务对象</returns>
        public ITransaction BeginTransaction()
        {
            return new NHTransaction(currentSession.BeginTransaction());
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">事务等级</param>
        /// <returns>事务对象</returns>
        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return new NHTransaction(currentSession.BeginTransaction(isolationLevel));
        }

        /// <summary>
        /// 回滚当前工作单元
        /// </summary>
        public void Rollback()
        {
            if (currentSession.Transaction != null
                && currentSession.Transaction.IsActive)
            {
                currentSession.Transaction.Rollback();
            }

            currentSession.Clear();
        }

        /// <summary>
        /// 提交工作单元
        /// </summary>
        public void Commit()
        {
            currentSession.Flush();

            if (currentSession.Transaction != null && currentSession.Transaction.IsActive)
            {
                currentSession.Transaction.Commit();
            }
        }

        /// <summary>
        /// NHibernate的Session
        /// </summary>
        public dynamic Session
        {
            get { return currentSession; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (currentSession != null)
                    {
                        if (currentSession.Transaction != null
                                && currentSession.Transaction.IsActive)
                        {
                            currentSession.Transaction.Rollback();
                            currentSession.Transaction.Dispose();
                        }
                        currentSession.Clear();
                        currentSession.Dispose();
                        currentSession = null;
                    }
                }
                disposed = true;
            }
        }
    }
}
