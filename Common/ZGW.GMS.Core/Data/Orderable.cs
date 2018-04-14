using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// Linq查询排序
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class Orderable<T> where T : class
    {
        private IQueryable<T> queryable;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enumerable">要进行排序的IQueryable对象</param>
        public Orderable(IQueryable<T> enumerable)
        {
            queryable = enumerable;
        }

        /// <summary>
        /// 查询接口
        /// </summary>
        public IQueryable<T> Queryable
        {
            get { return queryable; }
        }

        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="TKey">排序的键的类型</typeparam>
        /// <param name="keySelector">提供排序的键的Lambada表达示</param>
        /// <returns>排序后的Orderable</returns>
        public Orderable<T> Asc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            queryable = queryable
                .OrderBy(keySelector);
            return this;
        }

        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="TKey1">排序的键的类型1</typeparam>
        /// <typeparam name="TKey2">排序的键的类型2</typeparam>
        /// <param name="keySelector1">提供排序的键1的Lambada表达示</param>
        /// <param name="keySelector2">提供排序的键2的Lambada表达示</param>
        /// <returns>排序后的Orderable对象</returns>
        public Orderable<T> Asc<TKey1, TKey2>(Expression<Func<T, TKey1>> keySelector1,
                                              Expression<Func<T, TKey2>> keySelector2)
        {
            queryable = queryable
                .OrderBy(keySelector1)
                .OrderBy(keySelector2);
            return this;
        }

        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="TKey1">排序的键的类型1</typeparam>
        /// <typeparam name="TKey2">排序的键的类型2</typeparam>
        /// <typeparam name="TKey3">排序的键的类型3</typeparam>
        /// <param name="keySelector1">提供排序的键1的Lambada表达示1</param>
        /// <param name="keySelector2">提供排序的键1的Lambada表达示2</param>
        /// <param name="keySelector3">提供排序的键1的Lambada表达示3</param>
        /// <returns>排序后的Orderable对象</returns>
        public Orderable<T> Asc<TKey1, TKey2, TKey3>(Expression<Func<T, TKey1>> keySelector1,
                                                     Expression<Func<T, TKey2>> keySelector2,
                                                     Expression<Func<T, TKey3>> keySelector3)
        {
            queryable = queryable
                .OrderBy(keySelector1)
                .OrderBy(keySelector2)
                .OrderBy(keySelector3);
            return this;
        }


        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="TKey">排序的键的类型</typeparam>
        /// <param name="keySelector">提供排序的键的Lambada表达示</param>
        /// <returns>排序后的Orderable</returns>
        public Orderable<T> Desc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            queryable = queryable
                .OrderByDescending(keySelector);
            return this;
        }

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="TKey1">排序的键的类型1</typeparam>
        /// <typeparam name="TKey2">排序的键的类型2</typeparam>
        /// <param name="keySelector1">提供排序的键1的Lambada表达示</param>
        /// <param name="keySelector2">提供排序的键2的Lambada表达示</param>
        /// <returns>排序后的Orderable对象</returns>
        public Orderable<T> Desc<TKey1, TKey2>(Expression<Func<T, TKey1>> keySelector1,
                                               Expression<Func<T, TKey2>> keySelector2)
        {
            queryable = queryable
                .OrderByDescending(keySelector1)
                .OrderByDescending(keySelector2);
            return this;
        }

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="TKey1">排序的键的类型1</typeparam>
        /// <typeparam name="TKey2">排序的键的类型2</typeparam>
        /// <typeparam name="TKey3">排序的键的类型3</typeparam>
        /// <param name="keySelector1">提供排序的键1的Lambada表达示1</param>
        /// <param name="keySelector2">提供排序的键1的Lambada表达示2</param>
        /// <param name="keySelector3">提供排序的键1的Lambada表达示3</param>
        /// <returns>排序后的Orderable对象</returns>
        public Orderable<T> Desc<TKey1, TKey2, TKey3>(Expression<Func<T, TKey1>> keySelector1,
                                                      Expression<Func<T, TKey2>> keySelector2,
                                                      Expression<Func<T, TKey3>> keySelector3)
        {
            queryable = queryable
                .OrderByDescending(keySelector1)
                .OrderByDescending(keySelector2)
                .OrderByDescending(keySelector3);
            return this;
        }
    }
}
