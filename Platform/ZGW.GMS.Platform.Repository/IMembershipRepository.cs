using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Platform.BusinessEntity;

namespace ZGW.GMS.Platform.Repository
{
    public interface IMembershipRepository
    {
        /// <summary>
        /// 验证登陆的状态
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        // ValidationStatus Login(string userName, string password);
        ValidationStatus Login(string userName, string password);
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        List<User> GetListuser(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(User model);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        User GetModelByStrWhere(string strWhere);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        bool DeleteList(string IDlist);

        #region 角色
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int AddRole(Role model);
        /// <summary>
        /// 角色更新一条数据
        /// </summary>
        bool UpdateRole(Role model);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        bool DeleteListRole(string IDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Role GetModelRole(int ID);
        /// <summary>
        ///角色 分页获取数据
        /// </summary>
        /// <returns></returns>
        List<Role> GetListByPageToList(string strWhere, string orderby, int startIndex, int endIndex,bool isAll=false);
        #endregion
    }
}
