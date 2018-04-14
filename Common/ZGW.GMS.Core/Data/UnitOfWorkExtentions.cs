using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 工作单元的扩展方法
    /// </summary>
    public static class UnitOfWorkExtentions
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        public static void Create<T>(this IUnitOfWork unitOfWork,T entity) where T : class
        {
            unitOfWork.RegisterNew(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        public static void Update<T>(this IUnitOfWork unitOfWork,T entity) where T : class
        {
            unitOfWork.RegisterChanged(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        public static void Delete<T>(this IUnitOfWork unitOfWork,T entity) where T : class
        {
            unitOfWork.RegisterDeleted(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">需要删除记录的ID数组</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        public static void Delete<T>(this IUnitOfWork unitOfWork,int[] ids) where T : class
        {
            foreach (var id in ids)
            {
                var entity = unitOfWork.Get<T>(id);
                unitOfWork.RegisterDeleted(entity);
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public static void Flush(this IUnitOfWork unitOfWork)
        {
            unitOfWork.Commit();
        }

        /// <summary>
        /// 按ID获取
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>与ID对应的实体对象</returns>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        public static T Get<T>(this IUnitOfWork unitOfWork,int id) where T : class
        {
            return unitOfWork.Get<T>(id);
        }

        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <returns>符合符件的对象</returns>
        /// <typeparam name="T">实体类型</typeparam>
        public static T Get<T>(this IUnitOfWork unitOfWork,Expression<Func<T, bool>> predicate) where T : class
        {
            return unitOfWork.Query<T>()
                .FirstOrDefault(predicate);
        }

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>IQueryable对象</returns>
        public static IQueryable<T> Query<T>(this IUnitOfWork unitOfWork) where T : class
        {
            return unitOfWork.Query<T>();
        }

        /// <summary>
        /// 查询符合条件的记录数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>符合条件的记录数</returns>
        public static int Count<T>(this IUnitOfWork unitOfWork,Expression<Func<T, bool>> predicate) where T : class
        {
            return unitOfWork.Query<T>().Count(predicate);
        }

        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>符合条件的枚举</returns>
        public static IEnumerable<T> Fetch<T>(this IUnitOfWork unitOfWork,Expression<Func<T, bool>> predicate) where T : class
        {
            return unitOfWork.Query<T>().Where(predicate);
        }

        /// <summary>
        /// 根据查询条件获取排序后的数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="order">排序规则</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>符合条件的枚举</returns>
        public static IEnumerable<T> Fetch<T>(this IUnitOfWork unitOfWork,Expression<Func<T, bool>> predicate, Action<Orderable<T>> order) where T : class
        {
            var query = unitOfWork.Query<T>().Where(predicate);
            Orderable<T> orderable = new Orderable<T>(query);
            return orderable.Queryable;
        }

        /// <summary>
        /// 根据查询条件获取分页后的数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="order">排序规则</param>
        /// <param name="skip">开始记录数</param>
        /// <param name="count">查询多少条</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>符合条件的枚举</returns>
        public static IEnumerable<T> Fetch<T>(this IUnitOfWork unitOfWork,Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count) where T : class
        {
            return unitOfWork.Fetch(predicate, order)
                .Skip(skip)
                .Take(count);
        }
    }
}
