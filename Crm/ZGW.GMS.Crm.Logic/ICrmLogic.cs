using System;
using System.Collections.Generic;
using ZGW.GMS.Core;
using ZGW.GMS.Crm.BusinessEntity;
namespace ZGW.GMS.Crm.Logic
{
    public interface ICrmLogic
    {
       
        #region 一般用电用户
        /// <summary>
        /// 批量删除一般用户
        /// </summary>
        /// <returns></returns>
        bool DelPersonuser(string ids);

        /// <summary>
        /// 新增一般用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddPersonuser(ElectricityUser model);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
       // PagedTable GetPersonuser(string strWhere);

        ElectricityUser GetPersonmodel(int ID);
          /// <summary>
        /// 一般客户
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        List<ElectricityUser> GetListElectricityUser(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true);
        #endregion




        #region 公司用电用户
        /// <summary>
        /// 批量删除公司用户
        /// </summary>
        /// <returns></returns>
        bool DelCompany(string ids);

        /// <summary>
        /// 新增公司用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddCompany(Company model);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        //PagedTable GetCompany(string strWhere);

        Company GetCompanymodel(int ID);
         /// <summary>
        /// 获取公司分页数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="isAll">true:获取全部数据；false:分页获取数据</param>
        /// <returns></returns>
        List<Company> GetListCompany(string strWhere, string orderby, int startIndex, int endIndex, bool isAll = true);
        #endregion
    }
}
