using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Data
{
    /// <summary>
    /// 历史库的数据访问接口
    /// </summary>
    /// <typeparam name="T">数据实体类型</typeparam>
    public interface IRepositoryHistory<T> : IRepositoryBase<T> where T : class
    {
    }
}
