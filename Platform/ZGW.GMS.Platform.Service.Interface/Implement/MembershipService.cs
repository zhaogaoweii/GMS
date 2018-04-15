using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core;
using ZGW.GMS.Platform.BusinessEntity;
using ZGW.GMS.Platform.Logic;
namespace ZGW.GMS.Platform.Service.Interface.Implement
{
    [ComponentRegistry]
    public class MembershipService : IMembershipService
    {
        IMembershipLogic _iMembershipLogic;
        public MembershipService(IMembershipLogic iMembershipLogic)
        {
            this._iMembershipLogic = iMembershipLogic;
        }
        public ValidationStatus Login(string userName, string password)
        {
            return _iMembershipLogic.Login(userName, password);
        }  /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<User> GetListuser(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true)
        {
            return _iMembershipLogic.GetListuser(strWhere, orderby, startIndex, endIndex, isAll);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(User model)
        {
            return _iMembershipLogic.Add(model);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public User GetModelByStrWhere(string strWhere)
        {
            return _iMembershipLogic.GetModelByStrWhere(strWhere);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return _iMembershipLogic.DeleteList(IDlist);
        }

        #region 角色
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddRole(Role model)
        {
            return _iMembershipLogic.AddRole(model);
        }
        /// <summary>
        /// 角色更新一条数据
        /// </summary>
        public bool UpdateRole(Role model)
        {
            return _iMembershipLogic.UpdateRole(model);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteListRole(string IDlist)
        {
            return _iMembershipLogic.DeleteListRole(IDlist);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Role GetModelRole(int ID)
        {
            return _iMembershipLogic.GetModelRole(ID);
        }
        /// <summary>
        ///角色 分页获取数据
        /// </summary>
        /// <returns></returns>
        public List<Role> GetListByPageToList(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = false)
        {
            return _iMembershipLogic.GetListByPageToList(strWhere, orderby, startIndex, endIndex,isAll);
        }
        #endregion
    }
}
