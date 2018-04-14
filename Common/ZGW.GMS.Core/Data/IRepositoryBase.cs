using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 资源库的泛型接口
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    public interface IRepositoryBase<TEntity>:IDisposable where TEntity : class
    {
        /// <summary>
        /// 当前资源库的工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="unitOfWork">工作单元</param>
        void Create(TEntity entity,IUnitOfWork unitOfWork=null);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="unitOfWork">工作单元</param>
        void Update(TEntity entity, IUnitOfWork unitOfWork = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="unitOfWork">工作单元</param>
        void Delete(TEntity entity, IUnitOfWork unitOfWork = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">删除和的Id数组</param>
        /// <param name="unitOfWork">工作单元</param>
        void Delete(int[] ids, IUnitOfWork unitOfWork = null);

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">对象源</param>
        /// <param name="target">目标对象</param>
        /// <typeparam name="T">实体类型</typeparam>
        void Copy<T>(T source, T target) where T : class;

        /// <summary>
        /// 同步数据到数据库
        /// </summary>
        void Flush();

        /// <summary>
        /// 根据Id获取对象
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <returns>对应的实体对象</returns>
        TEntity Get(int id, IUnitOfWork unitOfWork = null);

        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <returns>符合符件的对象</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate, IUnitOfWork unitOfWork = null);

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <returns>查询接口</returns>
        IQueryable<TEntity> Query(IUnitOfWork unitOfWork=null);

        /// <summary>
        /// 查询符合条件的记录数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <returns>符合条件的记录数</returns>
        int Count(Expression<Func<TEntity, bool>> predicate, IUnitOfWork unitOfWork = null);

        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <returns>符合条件的枚举</returns>
        IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate, IUnitOfWork unitOfWork = null);

        /// <summary>
        /// 根据查询条件获取排序后的数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="order">排序规则</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <returns>符合条件的枚举</returns>
        IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order, IUnitOfWork unitOfWork = null);

        /// <summary>
        /// 根据查询条件获取分页后的数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="order">排序规则</param>
        /// <param name="skip">开始记录数</param>
        /// <param name="count">查询多少条</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <returns>符合条件的枚举</returns>
        IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order, int skip, int count, IUnitOfWork unitOfWork = null);
    }    
}
