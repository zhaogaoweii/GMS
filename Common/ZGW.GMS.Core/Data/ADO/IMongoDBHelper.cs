using ZGW.GMS.Core.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Data.ADO
{
    public interface IMongoDBHelper<T> where T : MongoDBEntity
    {
        IMongoDatabase GetMongoDataBase();

        IMongoCollection<T> GetMongoCols();


        #region  插入数据

        bool InsertOne(T Model);

        bool InsertOneAsync(T Model);

        bool InsertMany(IEnumerable<T> Models);

        bool InsertManyAsync(IEnumerable<T> Models);

        #endregion

        #region  更新数据

        #endregion
    }
}
