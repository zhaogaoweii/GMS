using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.MongoDB
{
    /// <summary>
    /// MongoDB实体基类
    /// </summary>
    [Serializable]
    [DataContract]
    public class MongoDBEntity
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        [DataMember]
        public ObjectId Id { get; set; }
    }
}
