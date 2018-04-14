using ZGW.GMS.Core.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Data.ADO
{
    /// <summary>
    /// MongoDB操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoDBHelper<T> : IMongoDBHelper<T> where T : MongoDBEntity, new()
    {

        #region  属性字段

        /// <summary>
        /// 集合名称
        /// </summary>
        public string ColsName
        {
            get;
            set;
        }

        /// <summary>
        /// 默认排序字段
        /// </summary>
        public string DefaultSort
        {
            get;
            set;
        }

        /// <summary>
        /// 降序
        /// </summary>
        public bool IsDescending
        {
            get;
            set;
        }

        #endregion


        #region  构造函数及基础方法
        /// <summary>
        /// 
        /// </summary>
        public MongoDBHelper()
        {
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ColsName"></param>
        public MongoDBHelper(string ColsName)
        {
            this.ColsName = ColsName;
        }

        /// <summary>
        /// MongoDB数据库实例
        /// </summary>
        public IMongoDatabase GetMongoDataBase()
        {
            string conn = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;

            string dbName = ConfigurationManager.AppSettings["MongoDBName"];

            MongoClient mgClient = new MongoClient(conn);

            if (null == mgClient)
                return null;

            return mgClient.GetDatabase(dbName);
        }


        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<T> GetMongoCols()
        {
            IMongoDatabase mg = GetMongoDataBase();

            if (null == mg)
                return null;

            return mg.GetCollection<T>(this.ColsName);
        }

        #endregion

        #region  对象添加、修改删除

        #region  插入数据

        /// <summary>
        /// 单文档插入（同步）
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool InsertOne(T Model)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            cls.InsertOne(Model);

            return true;
        }

        /// <summary>
        /// 单文档插入（异步）
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool InsertOneAsync(T Model)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            cls.InsertOneAsync(Model);

            return true;
        }

        /// <summary>
        /// 多文档插入（同步）
        /// </summary>
        /// <param name="Models"></param>
        /// <returns></returns>
        public bool InsertMany(IEnumerable<T> Models)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            cls.InsertMany(Models);

            return true;
        }

        /// <summary>
        /// 多文档插入（异步）
        /// </summary>
        /// <param name="Models"></param>
        /// <returns></returns>
        public bool InsertManyAsync(IEnumerable<T> Models)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            cls.InsertManyAsync(Models);

            return true;
        }

        #endregion

        #region  更新数据

        /// <summary>
        /// 根据文档ID整体更新
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool UpdateOne(T Model, string Id)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            //使用 IsUpsert = true ，如果没有记录则写入
            ReplaceOneResult rlt = cls.ReplaceOne<T>(o => o.Id == ObjectId.Parse(Id), Model, new UpdateOptions() { IsUpsert = true });

            if (null == rlt)
                return false;

            if (rlt.ModifiedCount <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 根据文档ID部分字段更新（同步）
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Update"></param>
        /// <returns></returns>
        public virtual bool UpdateOne(string Id, UpdateDefinition<T> Update)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            UpdateResult rlt = cls.UpdateOne<T>(o => o.Id.ToString() == Id, Update, new UpdateOptions() { IsUpsert = true });

            if (null == rlt)
                return false;

            if (rlt.ModifiedCount <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 根据文档ID部分字段更新（异步）
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Update"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOneAsync(string Id, UpdateDefinition<T> Update)
        {

            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return await Task.FromResult(false);

            Task<UpdateResult> rlt = cls.UpdateOneAsync<T>(o => o.Id.ToString() == Id, Update, new UpdateOptions() { IsUpsert = true });

            if (null == rlt)
                return await Task.FromResult(false);

            if (rlt.Result.ModifiedCount > 0)
                return await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 根据匹配条件部分字段更新（同步）
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="Update"></param>
        /// <returns></returns>
        public bool UpdateMany(FilterDefinition<T> Match, UpdateDefinition<T> Update)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            //使用 IsUpsert = true ，如果没有记录则写入
            UpdateResult rlt = cls.UpdateMany(Match, Update, new UpdateOptions() { IsUpsert = false });

            if (null == rlt)
                return false;

            if (rlt.ModifiedCount <= 0)
                return false;

            return true;
        }


        /// <summary>
        /// 根据匹配条件部分字段更新（异步）
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="Update"></param>
        /// <returns></returns>
        public async Task<bool> UpdateManyAsync(FilterDefinition<T> Match, UpdateDefinition<T> Update)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return await Task.FromResult(false);

            //使用 IsUpsert = true ，如果没有记录则写入
            Task<UpdateResult> rlt = cls.UpdateManyAsync(Match, Update, new UpdateOptions() { IsUpsert = false });

            if (null == rlt)
                return await Task.FromResult(false);

            if (rlt.Result.ModifiedCount > 0)
                return await Task.FromResult(false);

            return await Task.FromResult(true);

        }


        /// <summary>
        /// 根据Linq表达式部分字段更新（同步）
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="Update"></param>
        /// <returns></returns>
        public bool UpdateMany(Expression<Func<T, bool>> Match, UpdateDefinition<T> Update)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            //使用 IsUpsert = true ，如果没有记录则写入
            UpdateResult rlt = cls.UpdateMany<T>(Match, Update, new UpdateOptions() { IsUpsert = false });

            if (null == rlt)
                return false;

            if (rlt.ModifiedCount <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 根据Linq表达式部分字段更新（异步）
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="Update"></param>
        /// <returns></returns>
        public async Task<bool> UpdateManyAsync(Expression<Func<T, bool>> Match, UpdateDefinition<T> Update)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return await Task.FromResult(false);

            //使用 IsUpsert = true ，如果没有记录则写入
            Task<UpdateResult> rlt = cls.UpdateManyAsync<T>(Match, Update, new UpdateOptions() { IsUpsert = false });

            if (null == rlt)
                return await Task.FromResult(false);

            if (rlt.Result.ModifiedCount > 0)
                return await Task.FromResult(false);

            return await Task.FromResult(true);

        }

        #endregion

        #region  删除数据

        /// <summary>
        /// 根据文档ID删除文档
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteById(string Id)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            DeleteResult rlt = cls.DeleteOne(o => o.Id.ToString() == Id);

            if (null == rlt)
                return false;

            if (rlt.DeletedCount <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 根据文档ID集合删除文档
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public bool DeleteByIds(List<string> Ids)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            DeleteResult rlt = cls.DeleteMany(o => Ids.Contains(o.Id.ToString()));

            if (null == rlt)
                return false;

            if (rlt.DeletedCount <= 0)
                return false;

            return true;
        }


        /// <summary>
        /// 根据Linq表达式删除文档（同步）
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public bool DeleteMany(Expression<Func<T, bool>> Match)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            DeleteResult rlt = cls.DeleteMany(Match);

            if (null == rlt)
                return false;

            if (rlt.DeletedCount <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 根据Linq表达式删除文档（异步）
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public async Task<bool> DeleteManyAsync(Expression<Func<T, bool>> Match)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return await Task.FromResult(false);

            Task<DeleteResult> rlt = cls.DeleteManyAsync(Match);

            if (null == rlt)
                return await Task.FromResult(false);

            if (rlt.Result.DeletedCount > 0)
                return await Task.FromResult(false);

            return await Task.FromResult(true);
        }


        /// <summary>
        /// 根据匹配条件删除文档（同步）
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public bool DeleteMany(FilterDefinition<T> Match)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return false;

            DeleteResult rlt = cls.DeleteMany(Match);

            if (null == rlt)
                return false;

            if (rlt.DeletedCount <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 根据匹配条件删除文档（异步）
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public async Task<bool> DeleteManyAsync(FilterDefinition<T> Match)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return await Task.FromResult(false);

            Task<DeleteResult> rlt = cls.DeleteManyAsync(Match);

            if (null == rlt)
                return await Task.FromResult(false);

            if (rlt.Result.DeletedCount > 0)
                return await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        #endregion

        #endregion

        #region  查找数据

        #region  单文档

        /// <summary>
        /// 根据文档ID获取文档
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T FindByID(string Id)
        {

            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return null;

            return cls.Find(o => o.Id == ObjectId.Parse(Id)).FirstOrDefault();
        }


        /// <summary>
        /// 根据匹配条件获取第一个文档
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public T FindSingle(FilterDefinition<T> Match)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return null;

            return cls.Find(Match).FirstOrDefault();
        }

        /// <summary>
        /// 根据Linq表达式获取第一个文档
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public T FindSingle(Expression<Func<T, bool>> Match)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return null;

            return cls.Find(Match).FirstOrDefault();
        }


        /// <summary>
        /// 根据文档ID获取文档
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<T> FindByIDAsync(string Id)
        {

            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return null;

            return await cls.FindAsync(o => o.Id.ToString() == Id).Result.FirstOrDefaultAsync();
        }


        /// <summary>
        /// 根据匹配条件获取第一个文档
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public async Task<T> FindSingleAsync(FilterDefinition<T> Match)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return null;

            return await cls.FindAsync(Match).Result.FirstOrDefaultAsync();
        }

        #endregion

        #region  集合

        /// <summary>
        /// 根据Linq表达式获取文档记录数
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public long Count(Expression<Func<T, bool>> Match)
        {

            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return 0;

            return cls.Count<T>(Match);
        }

        /// <summary>
        /// 根据匹配条件获取文档记录数
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public long Count(FilterDefinition<T> Match)
        {

            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return 0;

            return cls.Count(Match);
        }

        /// <summary>
        /// 根据Linq表达式获取文档集合
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public IList<T> Find(Expression<Func<T, bool>> Match)
        {
            return GetQueryable(Match).ToList();
        }

        /// <summary>
        /// 根据匹配条件获取文档集合
        /// </summary>
        /// <param name="Match">条件表达式</param>
        /// <returns>指定对象的集合</returns>
        public IList<T> Find(FilterDefinition<T> Match)
        {
            return GetQueryable(Match).ToList();
        }


        /// <summary>
        /// 根据Linq表达式获取文档集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public IList<T> Find<TKey>(Expression<Func<T, bool>> Match, Expression<Func<T, TKey>> OrderBy, bool IsDescending = true)
        {
            return GetQueryable<TKey>(Match, OrderBy, IsDescending).ToList();
        }

        /// <summary>
        /// 根据匹配条件获取文档集合
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsDescending"></param>
        /// <returns></returns>
        public IList<T> Find(FilterDefinition<T> Match, string OrderBy, bool IsDescending = true)
        {
            return GetQueryable(Match, OrderBy, IsDescending).ToList();
        }

        /// <summary>
        /// 根据Linq表达式获取文档集合（异步）
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public async Task<IList<T>> FindAsync(Expression<Func<T, bool>> Match)
        {
            return await Task.FromResult(GetQueryable(Match).ToList());
        }

        /// <summary>
        /// 根据匹配条件获取文档集合（异步）
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public async Task<IList<T>> FindAsync(FilterDefinition<T> Match)
        {
            return await GetQueryable(Match).ToListAsync();
        }

        /// <summary>
        /// 根据Linq表达式获取文档集合
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="Match"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsDescending"></param>
        /// <returns></returns>
        public async Task<IList<T>> FindAsync<TKey>(Expression<Func<T, bool>> Match, Expression<Func<T, TKey>> OrderBy, bool IsDescending = true)
        {
            return await Task.FromResult(GetQueryable<TKey>(Match, OrderBy, IsDescending).ToList());
        }

        /// <summary>
        /// 根据匹配条件获取文档集合
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsDescending"></param>
        /// <returns></returns>
        public async Task<IList<T>> FindAsync(FilterDefinition<T> Match, string OrderBy, bool IsDescending = true)
        {
            return await GetQueryable(Match, OrderBy, IsDescending).ToListAsync();
        }

        #endregion

        #region  分页方法

        /// <summary>
        /// Linq表达式获取分页数据集合（同步）
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="Info"></param>
        /// <returns></returns>
        public IList<T> FindWithPager(Expression<Func<T, bool>> Match, PagerInfo Info)
        {
            int pageindex = (Info.CurrenetPageIndex < 1) ? 1 : Info.CurrenetPageIndex;

            int pageSize = (Info.PageSize <= 0) ? 20 : Info.PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IQueryable<T> query = GetQueryable(Match);

            Info.RecordCount = query.Count();

            return query.Skip(excludedRows).Take(pageSize).ToList();
        }

        /// <summary>
        /// 匹配条件获取分页数据集合（同步）
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="Info"></param>
        /// <returns></returns>
        public IList<T> FindWithPager(FilterDefinition<T> Match, PagerInfo Info)
        {
            int pageindex = (Info.CurrenetPageIndex < 1) ? 1 : Info.CurrenetPageIndex;

            int pageSize = (Info.PageSize <= 0) ? 20 : Info.PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IFindFluent<T, T> find = GetQueryable(Match);

            Info.RecordCount = (int)find.Count();

            return find.Skip(excludedRows).Limit(pageSize).ToList();
        }

        /// <summary>
        /// Linq表达式获取分页数据集合（异步）
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="Info"></param>
        /// <returns></returns>
        public async Task<IList<T>> FindWithPagerAsync(Expression<Func<T, bool>> Match, PagerInfo Info)
        {
            int pageindex = (Info.CurrenetPageIndex < 1) ? 1 : Info.CurrenetPageIndex;

            int pageSize = (Info.PageSize <= 0) ? 20 : Info.PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IQueryable<T> query = GetQueryable(Match);

            Info.RecordCount = query.Count();

            IList<T> result = query.Skip(excludedRows).Take(pageSize).ToList();

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 匹配条件获取分页数据集合（异步）
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="Info"></param>
        /// <returns></returns>
        public async Task<IList<T>> FindWithPagerAsync(FilterDefinition<T> Match, PagerInfo Info)
        {
            int pageindex = (Info.CurrenetPageIndex < 1) ? 1 : Info.CurrenetPageIndex;

            int pageSize = (Info.PageSize <= 0) ? 20 : Info.PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IFindFluent<T, T> find = GetQueryable(Match);

            Info.RecordCount = (int)find.Count();

            return await find.Skip(excludedRows).Limit(pageSize).ToListAsync();
        }

        #endregion

        #region  基础方法

        /// <summary>
        /// 返回可查询的记录源
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetQueryable()
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return null;

            IQueryable<T> query = cls.AsQueryable();

            //if (null == query)
            //    return null;

            //query = this.IsDescending ? query.OrderByDescending(o => this.DefaultSort) : query.OrderBy(o => this.DefaultSort);

            return query;
        }

        /// <summary>
        /// 根据Linq表达式获取查询记录源
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> Match)
        {
            IQueryable<T> query = GetQueryable();

            if (null == query)
                return null;

            query = query.Where(Match);

            if (null == query)
                return null;

            query = query.OrderBy(this.DefaultSort, this.IsDescending);

            return query;
        }

        /// <summary>
        /// 根据Linq表达式获取查询记录源
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsDescending"></param>
        /// <returns></returns>
        public IQueryable<T> GetQueryable<Tkey>(Expression<Func<T, bool>> Match, Expression<Func<T, Tkey>> OrderBy, bool IsDescending = true)
        {
            IQueryable<T> query = GetQueryable();

            if (null == query)
                return null;

            query = query.Where(Match);

            if (null == query)
                return null;

            if (OrderBy == null)
                query = query.OrderBy(this.DefaultSort, this.IsDescending);
            else
                query = IsDescending ? query.OrderByDescending(OrderBy) : query.OrderBy(OrderBy);

            return query;
        }

        /// <summary>
        /// 根据匹配条件获取查询记录源
        /// </summary>
        /// <param name="Match"></param>
        /// <returns></returns>
        public IFindFluent<T, T> GetQueryable(FilterDefinition<T> Match)
        {
            return GetQueryable(Match, this.DefaultSort, this.IsDescending);
        }

        /// <summary>
        /// 根据匹配条件获取查询记录源
        /// </summary>
        /// <param name="Match"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsDescending"></param>
        /// <returns></returns>
        public IFindFluent<T, T> GetQueryable(FilterDefinition<T> Match, string OrderBy, bool IsDescending = true)
        {
            IMongoCollection<T> cls = GetMongoCols();

            if (null == cls)
                return null;

            IFindFluent<T, T> queryable = cls.Find(Match);

            SortDefinition<T> sort = this.IsDescending ? Builders<T>.Sort.Descending(OrderBy) : Builders<T>.Sort.Ascending(OrderBy);

            return queryable.Sort(sort);
        }


        #endregion

        #endregion

    }
}
