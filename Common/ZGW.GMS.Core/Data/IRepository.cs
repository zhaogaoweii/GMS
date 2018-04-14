using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;
using ZGW.GMS.Core;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 资源库接口
    /// </summary>
    /// <typeparam name="T">数据实体类型</typeparam>
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
