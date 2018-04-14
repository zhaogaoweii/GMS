using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 数据访问实体
    /// </summary>
    /// <typeparam name="TID">主键类型</typeparam>
    [DataContract]
    [Serializable]
    public class DomainEntity<TID>
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        [XmlIgnore]
        public virtual TID Id { get; set; }
    }

    /// <summary>
    /// 针对生产库的Entity
    /// </summary>
    [Serializable]
    public class DomainEntity : DomainEntity<int>
    {
    }

    /// <summary>
    /// 针对历史库的Entity
    /// </summary>
    [Serializable]
    public class HistoryEntity : DomainEntity<int>
    {
    }

    /// <summary>
    /// 每一个DB表必须满足的字段
    /// </summary>
    public interface IDBEntity
    {
        /// <summary>
        /// 创建者
        /// </summary>
        int? CreatorId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后操作者
        /// </summary>
        int? LastOperatorId { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDelete { get; set; }

        /// <summary>
        /// 记录版本
        /// </summary>
        int Version { get; set; }
    }

    /// <summary>
    /// 运行库的领域实体父类
    /// </summary>
    [Serializable]
    [DataContract]
    public class DBEntity : DomainEntity, IDBEntity
    {
        /// <summary>
        /// 创建者
        /// </summary>
        [DataMember]
        public virtual int? CreatorId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后操作者
        /// </summary>
        [DataMember]
        public virtual int? LastOperatorId { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public virtual DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        public virtual bool IsDelete { get; set; }

        /// <summary>
        /// 记录版本
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        /// 设置由谁删除项目
        /// </summary>
        /// <param name="operatorId">操作人的ID</param>
        public virtual void DeleteBy(int? operatorId)
        {
            DeleteBy(operatorId, DateTime.Now);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operateTime">操作时间</param>
        public virtual void DeleteBy(int? operatorId, DateTime? operateTime)
        {
            IsDelete = true;
            LastOperatorId = operatorId;
            LastUpdateTime = operateTime;
        }

        /// <summary>
        /// 设置由谁更新项目
        /// </summary>
        /// <param name="operatorId">操作人的ID</param>
        public virtual void UpdateBy(int? operatorId)
        {
            UpdateBy(operatorId, DateTime.Now);
        }

        /// <summary>
        /// 设置由谁更新项目
        /// </summary>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operateTime">操作时间</param>
        public virtual void UpdateBy(int? operatorId, DateTime? operateTime)
        {
            LastOperatorId = operatorId;
            LastUpdateTime = operateTime;
        }

        /// <summary>
        /// 设置由谁创建项目
        /// </summary>
        /// <param name="operatorId">操作人的ID</param>
        public virtual void CreateBy(int? operatorId)
        {
            CreateBy(operatorId, DateTime.Now);
        }

        /// <summary>
        /// 设置由谁创建项目
        /// </summary>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operateTime">操作时间</param>
        public virtual void CreateBy(int? operatorId, DateTime? operateTime)
        {
            CreatorId = operatorId;
            CreateTime = operateTime;
            LastOperatorId = operatorId;
            LastUpdateTime = operateTime;
        }

        /// <summary>
        /// 复制操作数据
        /// </summary>
        /// <param name="target">复制到的目标对象</param>
        public virtual void CopyOperation(IDBEntity target)
        {
            target.CreatorId = CreatorId;
            target.CreateTime = CreateTime;
            target.IsDelete = IsDelete;
            target.LastOperatorId = LastOperatorId;
            target.LastUpdateTime = LastUpdateTime;
        }
    }

    /// <summary>
    /// 历史库的领域实体父类
    /// </summary>
    [Serializable]
    [DataContract]
    public class DBHistoryEntity : HistoryEntity, IDBEntity
    {
        /// <summary>
        /// 创建者
        /// </summary>
        [DataMember]
        public virtual int? CreatorId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后操作者
        /// </summary>
        [DataMember]
        public virtual int? LastOperatorId { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public virtual DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        public virtual bool IsDelete { get; set; }

        /// <summary>
        /// 记录版本
        /// </summary>
        public virtual int Version { get; set; }
    }
}
