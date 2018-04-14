using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGW.GMS.Core;
using ZGW.GMS.Crm.BusinessEntity;
using ZGW.GMS.Crm.Logic;

namespace ZGW.GMS.Crm.Service.Interface.Implement
{
    [ComponentRegistry]
    public class CrmService : ICrmService
    {
        private ICrmLogic _iCrmLogic;
        public CrmService(ICrmLogic iCrmLogic)
        {
            this._iCrmLogic = iCrmLogic;
        }

        #region 一般用电用户
        /// <summary>
        /// 批量删除一般用户
        /// </summary>
        /// <returns></returns>
        public bool DelPersonuser(string ids)
        {
            return _iCrmLogic.DelPersonuser(ids);
        }
        /// <summary>
        /// 新增一般用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddPersonuser(ElectricityUser model)
        {
            return _iCrmLogic.AddPersonuser(model);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        //public PagedTable GetPersonuser(string strWhere)
        //{
        //    return _iCrmLogic.GetPersonuser(strWhere);
        //}
        public ElectricityUser GetPersonmodel(int ID)
        {
            return _iCrmLogic.GetPersonmodel(ID);
        }
        /// <summary>
        /// 一般客户
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<ElectricityUser> GetListElectricityUser(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true)
        {
            return _iCrmLogic.GetListElectricityUser(strWhere, orderby, startIndex, endIndex, isAll); 
        }
        #endregion




        #region 公司用电用户
        /// <summary>
        /// 批量删除公司用户
        /// </summary>
        /// <returns></returns>
        public bool DelCompany(string ids)
        {
            return _iCrmLogic.DelCompany(ids);
        }
        /// <summary>
        /// 新增公司用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddCompany(Company model)
        {
            return _iCrmLogic.AddCompany(model);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        //public PagedTable GetCompany(string strWhere)
        //{
        //    return _iCrmLogic.GetCompany(strWhere);
        //}
        public Company GetCompanymodel(int ID)
        {
            return _iCrmLogic.GetCompanymodel(ID);
        }
         /// <summary>
        /// 获取公司分页数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="isAll">true:获取全部数据；false:分页获取数据</param>
        /// <returns></returns>
        public List<Company> GetListCompany(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true) 
        {
            return _iCrmLogic.GetListCompany(strWhere,orderby,startIndex,endIndex,isAll);
        }
        #endregion

    }
}
