using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ZGW.GMS.Core.Data
{
    ///// <summary>
    ///// 资源库基类
    ///// </summary>
    ///// <typeparam name="TEntity">实体类型</typeparam>
    //public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>,IDisposable where TEntity : class
    //{
    //    private IUnitOfWork unitOfWork;
    //    private bool disposed;

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="unitOfWork">工作单元</param>
    //    public RepositoryBase(IUnitOfWork unitOfWork)
    //    {
    //        this.unitOfWork = unitOfWork;
    //    }

    //    /// <summary>
    //    /// 工作单元
    //    /// </summary>
    //    public virtual IUnitOfWork UnitOfWork
    //    {
    //        get { return unitOfWork; }
    //    }

    //    ///// <summary>
    //    ///// NHibernate的Session
    //    ///// </summary>
    //    //protected virtual ISession Session
    //    //{
    //    //    get { return unitOfWork.Session; }
    //    //}

    //    ///// <summary>
    //    ///// 复制
    //    ///// </summary>
    //    ///// <param name="source">对象源</param>
    //    ///// <param name="target">目标对象</param>
    //    ///// <typeparam name="T">实体类型</typeparam>
    //    //public virtual void Copy<T>(T source, T target) where T : class
    //    //{
    //    //    var metadata = Session.SessionFactory.GetClassMetadata(typeof(T));
    //    //    var values = metadata.GetPropertyValues(source, EntityMode.Poco);
    //    //    metadata.SetPropertyValues(target, values, EntityMode.Poco);
    //    //}

    //    /// <summary>
    //    /// 创建
    //    /// </summary>
    //    /// <param name="entity">实体</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    public virtual void Create(TEntity entity,IUnitOfWork unitOfWork=null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        unitOfWork.Create<TEntity>(entity);
    //    }

    //    /// <summary>
    //    /// 更新
    //    /// </summary>
    //    /// <param name="entity">实体</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    public virtual void Update(TEntity entity, IUnitOfWork unitOfWork = null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        unitOfWork.Update<TEntity>(entity);
    //    }

    //    /// <summary>
    //    /// 删除
    //    /// </summary>
    //    /// <param name="entity">实体</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    public virtual void Delete(TEntity entity, IUnitOfWork unitOfWork = null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        unitOfWork.Delete<TEntity>(entity);
    //    }

    //    /// <summary>
    //    /// 删除
    //    /// </summary>
    //    /// <param name="ids">需要删除记录的ID数组</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    public virtual void Delete(int[] ids, IUnitOfWork unitOfWork = null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        unitOfWork.Delete<TEntity>(ids);
    //    }

    //    /// <summary>
    //    /// 提交
    //    /// </summary>
    //    public virtual void Flush()
    //    {
    //        UnitOfWork.Flush();
    //    }

    //    /// <summary>
    //    /// 按ID获取
    //    /// </summary>
    //    /// <param name="id">ID</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    /// <returns>与ID对应的实体对象</returns>
    //    public virtual TEntity Get(int id, IUnitOfWork unitOfWork = null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        return unitOfWork.Get<TEntity>(id);
    //    }

    //    /// <summary>
    //    /// 根据查询条件获取数据
    //    /// </summary>
    //    /// <param name="predicate">查询条件</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    /// <returns>符合符件的对象</returns>
    //    public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate, IUnitOfWork unitOfWork = null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        return unitOfWork.Query<TEntity>().FirstOrDefault(predicate);
    //    }

    //    /// <summary>
    //    /// 查询接口
    //    /// </summary>
    //    /// <param name="unitOfWork">工作单元</param>
    //    /// <returns>IQueryable对象</returns>
    //    public virtual IQueryable<TEntity> Query(IUnitOfWork unitOfWork=null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        return unitOfWork.Query<TEntity>();
    //    }

    //    /// <summary>
    //    /// 查询符合条件的记录数
    //    /// </summary>
    //    /// <param name="predicate">查询条件</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    /// <returns>符合条件的记录数</returns>
    //    public virtual int Count(Expression<Func<TEntity, bool>> predicate, IUnitOfWork unitOfWork = null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        return unitOfWork.Count<TEntity>(predicate);
    //    }

    //    /// <summary>
    //    /// 根据查询条件获取数据
    //    /// </summary>
    //    /// <param name="predicate">查询条件</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    /// <returns>符合条件的枚举</returns>
    //    public virtual IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate, IUnitOfWork unitOfWork = null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        return unitOfWork.Fetch<TEntity>(predicate);
    //    }

    //    /// <summary>
    //    /// 根据查询条件获取排序后的数据
    //    /// </summary>
    //    /// <param name="predicate">查询条件</param>
    //    /// <param name="order">排序规则</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    /// <returns>符合条件的枚举</returns>
    //    public virtual IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order, IUnitOfWork unitOfWork = null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        return unitOfWork.Fetch<TEntity>(predicate, order);
    //    }

    //    /// <summary>
    //    /// 根据查询条件获取分页后的数据
    //    /// </summary>
    //    /// <param name="predicate">查询条件</param>
    //    /// <param name="order">排序规则</param>
    //    /// <param name="skip">开始记录数</param>
    //    /// <param name="count">查询多少条</param>
    //    /// <param name="unitOfWork">工作单元</param>
    //    /// <returns>符合条件的枚举</returns>
    //    public virtual IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order, int skip, int count, IUnitOfWork unitOfWork = null)
    //    {
    //        unitOfWork = unitOfWork ?? UnitOfWork;
    //        return unitOfWork.Fetch<TEntity>(predicate, order, skip, count);
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    ~RepositoryBase()
    //    {
    //        Dispose(false);
    //    }

    //    private void Dispose(bool disposing)
    //    {
    //        if (!disposed)
    //        {
    //            if (disposing)
    //            {
    //                if (unitOfWork != null)
    //                {
    //                    unitOfWork.Dispose();
    //                    unitOfWork = null;
    //                }
    //            }
    //            disposed = true;
    //        }
    //    }
    //}
}