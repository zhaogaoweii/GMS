using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Mvc.Navigation
{
    /// <summary>
    /// 导航的服务接口
    /// </summary>
    interface INavigationService
    {
        /// <summary>
        /// 根据用户ID加载有权限的导航节点
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>导航节点集合</returns>
        ICollection<INavigationNode> Load(int userID);

        /// <summary>
        /// 根据用户ID和指定的导航节点加载有权限的导航节点，用于异步展开导航UI
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="nodeID">导航节点ID</param>
        /// <returns>导航节点集合</returns>
        ICollection<INavigationNode> Load(int userID, int nodeID);
    }
}
