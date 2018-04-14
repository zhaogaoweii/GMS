using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGW.GMS.Core;
using ZGW.GMS.Platform.BusinessEntity;
using ZGW.GMS.Platform.Repository;
namespace ZGW.GMS.Platform.Logic.Implement
{
    [ComponentRegistry]
    public class MembershipLogic : IMembershipLogic
    {
        IMembershipRepository _iMembershipRepository;
        public MembershipLogic(IMembershipRepository iMembershipRepository)
        {
            this._iMembershipRepository = iMembershipRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ValidationStatus Login(string userName, string password)
        {
            return _iMembershipRepository.Login(userName, password);
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<User> GetListuser(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true)
        {
            return _iMembershipRepository.GetListuser(strWhere, orderby, startIndex, endIndex, isAll);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(User model)
        {
            return _iMembershipRepository.Add(model);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public User GetModelByStrWhere(string strWhere)
        {
            return _iMembershipRepository.GetModelByStrWhere(strWhere);
        }


        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return _iMembershipRepository.DeleteList(IDlist);
        }
    }
}
