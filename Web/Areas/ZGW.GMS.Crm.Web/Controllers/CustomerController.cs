using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGW.GMS.Core.PagedListUser;
using ZGW.GMS.Crm.BusinessEntity;
using ZGW.GMS.Crm.Service.Interface;

namespace ZGW.GMS.Crm.Web.Controllers
{
    [ZGW.GMS.Core.Mvc.Filters.AuthorizeFilter]
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        ICrmService _iCrmService;
        public CustomerController(ICrmService iCrmService)
        {
            this._iCrmService = iCrmService;
        }
        /// <summary>
        /// 获取公司用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyIndex()
        {
            int pageIndex = 0;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                pageIndex = Convert.ToInt32(Request["pageIndex"]);
            }
            List<Company> list = _iCrmService.GetListCompany(string.Empty, string.Empty, 0, 0, true);// _iCrmService.GetPersonmodel("", "", pageIndex, 12);

            return View(list.ToPagedList<Company>(pageIndex, 20));

        }
        /// <summary>
        /// 新增或者编辑公司用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult CompanyEdit(int ID = 0)
        {
            Company model = new Company();
            if (ID != 0)
            {
                model = _iCrmService.GetCompanymodel(ID);
            }
            return View(model);
        }
        /// <summary>
        /// 新增或者编辑公司用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompanyEdit(Company model)
        {
            string result = "失败";
            model.lastOperateTime = DateTime.Now;
            model.operatorTime = DateTime.Now;
            int count = _iCrmService.AddCompany(model);
            if (count > 0)
            {
                result = "成功";
            }
            return this.RefreshParent(result);
        }
        /// <summary>
        /// 获取一般用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ElectricityUserIndex()
        {
            int pageIndex = 0;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                pageIndex = Convert.ToInt32(Request["pageIndex"]);
            }
            List<ElectricityUser> list = _iCrmService.GetListElectricityUser(string.Empty, string.Empty, 0, 0, true);// _iCrmService.GetPersonmodel("", "", pageIndex, 12);

            return View(list.ToPagedList<ElectricityUser>(pageIndex, 20));

        }
        /// <summary>
        /// 新增或者编辑一般客户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult ElectricityUserEdit(int ID = 0)
        {
            ElectricityUser model = new ElectricityUser();
            if (ID != 0)
            {
                model = _iCrmService.GetPersonmodel(ID);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ElectricityUserEdit(ElectricityUser model)
        {
            string result = "失败";
            model.isDel = 0;
            model.lastOperateTime = DateTime.Now;
            model.operatorTime = DateTime.Now;
            int count = _iCrmService.AddPersonuser(model);
            if (count > 0)
            {
                result = "成功";
            }
            return this.RefreshParent(result);
        }
        [HttpPost]
        public ActionResult DelCompany(string ids)
        {
            string result = "失败";
            bool isOk = _iCrmService.DelCompany(ids);
            if (isOk)
            {
                result = "成功";
            }
            return this.RefreshParent(result);
        }

        [HttpPost]
        public ActionResult DelPerson(string ids)
        {

            string result = "失败";
            bool isOk = _iCrmService.DelPersonuser(ids);
            if (isOk)
            {
                result = "成功";
            }
            return this.RefreshParent(result);
        }
        /// <summary>
        /// 当弹出DIV弹窗时，需要刷新浏览器整个页面
        /// </summary>
        /// <returns></returns>
        public ContentResult RefreshParent(string alert = null)
        {
            var script = string.Format("<script>{0}; parent.location.reload(1)</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
            return this.Content(script);
        }
    }
}
